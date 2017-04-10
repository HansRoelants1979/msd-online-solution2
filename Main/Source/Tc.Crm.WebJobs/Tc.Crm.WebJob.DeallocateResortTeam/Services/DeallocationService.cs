﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Tc.Crm.WebJob.DeallocateResortTeam.Models;
using Tc.Crm.Common;
using Tc.Crm.Common.Services;
using Tc.Crm.Common.Constants;
using Tc.Crm.Common.Models;
using System.Collections.ObjectModel;
using BookingAttribute = Tc.Crm.Common.Constants.Attributes.Booking;

namespace Tc.Crm.WebJob.DeallocateResortTeam.Services
{
    public class DeallocationService : IDeallocationService
    {
        ICrmService crmService;
        ILogger logger;

        public DeallocationService(ILogger logger, ICrmService crmService)
        {
            this.logger = logger;
            this.crmService = crmService;
        }

        public DeallocationExecutionRequest FetchBookingsForDeallocation(DeallocationRequest bookingDeallocationRequest)
        {
            //logger.LogInformation("GetBookingAllocations - start");

            // don't process invalid request
            if (bookingDeallocationRequest == null || bookingDeallocationRequest.Destination == null ||
                bookingDeallocationRequest.Destination.Count == 0 ||
                bookingDeallocationRequest.Date == null || bookingDeallocationRequest.Date == DateTime.MinValue || bookingDeallocationRequest.Date == DateTime.MaxValue)
                return null;

            var destinationGateWays = GetGatewaysFilter(bookingDeallocationRequest.Destination);
            // will fail in case 2 default teams are set
            // get ids of bookings, filtered by return date and destination gateway, ordered by booking id, whose owner is hotel team
            // via customerbookingrole ordered by customer:
            // - get ids of contacts of booking and ids of their active incidents
            // - get ids of accounts of booking and ids of their active incidents
            // via country and business unit get default team id
            var query = string.Format(
                @"<fetch distinct='true' mapping='logical' output-format='xml-platform' version='1.0'>
                    <entity name='tc_booking'>
                    <attribute name='tc_bookingid' />
                    <filter type='and'>
                        <condition attribute='tc_returndate' operator='on' value='{0}' />
                        <condition attribute='tc_destinationgatewayid' operator='in'>
                        {1}
                        </condition>
                    </filter>
                    <order attribute='tc_bookingid' />
                    <link-entity name='team' from='teamid' to='owningteam' link-type='inner'>
                        <filter type='and'>
                        <condition attribute='tc_hotelteam' operator='eq' value='1' />
                        </filter>
                    </link-entity>
                    <link-entity name='tc_customerbookingrole' from='tc_bookingid' to='tc_bookingid' link-type='outer'>
                        <order attribute='tc_customer' />
                        <link-entity name='contact' from='contactid' to='tc_customer' link-type='outer' alias='contact' visible='true'>
                            <attribute name='contactid' />
                            <link-entity name='incident' from='customerid' to='contactid' link-type='outer' alias='contactIncident' visible='true'>
                                <attribute name='incidentid' />
                                <filter type='and'>
                                    <condition attribute='statecode' operator='eq' value='0' />
                                </filter>
                            </link-entity>
                        </link-entity>
                        <link-entity name='account' from='accountid' to='tc_customer' link-type='outer' alias='account'>
                            <attribute name='accountid' />
                            <link-entity name='incident' from='customerid' to='accountid' link-type='outer' alias='accountIncident' visible='true'>
                                <attribute name='incidentid' />
                                <filter type='and'>
                                    <condition attribute='statecode' operator='eq' value='0' />
                                </filter>
                            </link-entity>
                        </link-entity>
                    </link-entity>
                    <link-entity name='tc_country' from='tc_countryid' to='tc_sourcemarketid' link-type='inner'>
                        <link-entity name='businessunit' from='businessunitid' to='tc_sourcemarketbusinessunitid' link-type='inner'>
                            <link-entity name='team' from='businessunitid' to='businessunitid' link-type='inner' alias='defaultTeam' visible='true'>
                                <attribute name='teamid' />
                                <filter>
                                    <condition attribute='isdefault' operator='eq' value='1' />
                                </filter>
                            </link-entity>
                        </link-entity>
                    </link-entity>
                    </entity>
                </fetch>",
                new object[] { bookingDeallocationRequest.Date.ToString("yyyy-MM-dd"),
                destinationGateWays.ToString() });

            EntityCollection bookingCollection = crmService.RetrieveMultipleRecordsFetchXml(query);
            var result = ConvertCrmResponse(bookingCollection);
            return result;
        }

        public void DeallocateEntities(DeallocationExecutionRequest request)
        {
            var assignRequests = new Collection<AssignInformation>();
            // bookings
            CreateAssignRequests(assignRequests, request.Bookings);
            // customer
            CreateAssignRequests(assignRequests, request.Customers);
            // cases
            CreateAssignRequests(assignRequests, request.Cases);
            // assign
            if (assignRequests.Count > 0)
                crmService.BulkAssign(assignRequests);
        }

        private StringBuilder GetGatewaysFilter(IEnumerable<Guid> destinationGateways)
        {
            StringBuilder gateways = new StringBuilder();
            foreach (var destinationGateway in destinationGateways)
            {
                gateways.Append("<value>" + destinationGateway.ToString() + "</value>");
            }
            return gateways;
        }

        /// <summary>
        /// Covert CRM search results to collections of entities
        /// Implementation is based on assumption, that there cannot be one customer in 2 on-going bookings
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private DeallocationExecutionRequest ConvertCrmResponse(EntityCollection collection)
        {
            if (collection == null || collection.Entities.Count == 0)
                return null;

            var result = new DeallocationExecutionRequest
            {
                Bookings = new HashSet<Booking>(),
                Customers = new HashSet<Customer>(),
                Cases = new HashSet<Case>()
            };

            var currentBookingId = Guid.Empty;
            var currentCustomerId = Guid.Empty;

            foreach (var searchRecord in collection.Entities)
            {
                // TODO: warn on owner does not existing and don't process record
                var owner = new Owner {
                    Id = Guid.Parse(((AliasedValue)searchRecord["defaultTeam.teamid"]).Value.ToString()),
                    OwnerType = OwnerType.Team
                };

                var bookingId = Guid.Parse(searchRecord[BookingAttribute.BookingId].ToString());
                if (currentBookingId != bookingId)
                {
                    // add booking entity
                    var booking = new Booking
                    {
                        Id = bookingId,
                        Owner = owner
                    };                    

                    currentBookingId = bookingId;
                    currentCustomerId = Guid.Empty;

                    result.Bookings.Add(booking);
                }
                if (searchRecord.Contains("contact.contactid") || searchRecord.Contains("account.accountid"))
                {
                    var isContact = searchRecord.Contains("contact.contactid");
                    var customerId = Guid.Parse(((AliasedValue)searchRecord[isContact ? "contact.contactid" : "account.accountid"]).Value.ToString());
                    if (customerId != currentCustomerId)
                    {
                        // add customer entity
                        var customer = new Customer
                        {
                            Id = customerId,
                            Owner = owner,
                            CustomerType = isContact ? CustomerType.Contact : CustomerType.Account
                        };
                        currentCustomerId = customerId;
                        // IMPORTANT: assuming customer can be present in one booking
                        // TODO: check performance hit for check existance in result.Customers set. Can be added if not big impact so eliminating condition above
                        // should be also considered for cases
                        result.Customers.Add(customer);
                    }
                }
                if (searchRecord.Contains("contactIncident.incidentid") || searchRecord.Contains("accountIncident.incidentid"))
                {
                    var isContact = searchRecord.Contains("contactIncident.incidentid");
                    var incidentId = Guid.Parse(((AliasedValue)searchRecord[isContact ? "contactIncident.incidentid" : "accountIncident.incidentid"]).Value.ToString());
                    // add case entity
                    var incident = new Case
                    {
                        Id = incidentId,
                        Owner = owner
                    };
                    // IMPORTANT: Same case as for customer. Will fail if customer in second booking
                    result.Cases.Add(incident);
                }
            }

            return result;
        }

        private void CreateAssignRequests(Collection<AssignInformation> requets, IEnumerable<EntityModel> entities)
        {
            foreach (var entity in entities)
            {
                requets.Add(new AssignInformation
                {
                    EntityName = entity.EntityName,
                    RecordId = entity.Id,
                    RecordOwner = entity.Owner
                });
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeObject(crmService);
                    DisposeObject(logger);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DeallocateService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        void DisposeObject(Object obj)
        {
            if (obj != null)
            {
                if (obj is IDisposable)
                    ((IDisposable)obj).Dispose();
                else
                    obj = null;
            }

        }

        #endregion
    }
}
