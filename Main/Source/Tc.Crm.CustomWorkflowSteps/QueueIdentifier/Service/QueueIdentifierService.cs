﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Crm.CustomWorkflowSteps.QueueIdentifier.Model;

namespace Tc.Crm.CustomWorkflowSteps.QueueIdentifier.Service
{
    public class QueueIdentifierService
    {
        public EntityReference GetQueueBy(string queueName, IOrganizationService service, ITracingService trace)
        {
            if (string.IsNullOrWhiteSpace(queueName)) return null;
            if (service == null) return null;
            if (trace == null) return null;

            trace.Trace("GetQueueBy - start");
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              < entity name = 'queue' >
                                 < attribute name = 'queueid' />
                                  < filter type = 'and' >
                                     < condition attribute = 'name' operator= 'eq' value = '{0}' />
                                       </ filter >
                                     </ entity >
                                   </ fetch > ";

            fetchXml = string.Format(fetchXml, queueName);
            var query = new FetchExpression(fetchXml);
            var response = service.RetrieveMultiple(query);

            if (response == null || response.Entities == null || response.Entities.Count == 0)
            {
                trace.Trace("response is null or response.Entities is null or count is null.");
                return null;
            }

            if (response.Entities.Count > 1)
                throw new InvalidPluginExecutionException("Multiple queues exist with the same name.");

            trace.Trace("GetQueueBy - end");
            return new EntityReference(EntityName.Queue, response.Entities[0].Id);

        }
        
        public EntityReference GetQueueFor(EntityReference caseId, IOrganizationService service, ITracingService trace)
        {
            if (caseId == null) return null;
            if (service == null) return null;
            if (trace == null) return null;

            trace.Trace("GetQueueFor - start");
            var caseDetails = GetCaseDetailsFor(caseId, service, trace);
            if (caseDetails == null)
            {
                trace.Trace("case details is null.");
                return null;
            }

            var userRoles = GetRolesFor(caseDetails.Owner,service,trace);
            if (userRoles == null || userRoles.Count == 0) 
            {
                trace.Trace("There are no user roles for the user.");
                return null;
            }

            var department = GetDepartmentFrom(userRoles, trace);
            if (department == -1)
            {
                trace.Trace("department could not be determined from the user roles.");
                return null;
            }

            var sourceMarket = GetSourceMarketFrom(caseDetails,trace);
            if (sourceMarket == Guid.Empty)
            {
                trace.Trace("Source market could not be determined from the case record.");
                return null;
            }
            
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='queue'>
                                <attribute name='queueid' />
                                <filter type='and'>
                                  <condition attribute='tc_department' operator='eq' value='{0}' />
                                  <condition attribute='tc_sourcemarket' operator='eq' value='{1}' />
                                </filter>
                              </entity>
                            </fetch>";

            fetchXml = string.Format(fetchXml, department, sourceMarket.ToString());
            var query = new FetchExpression(fetchXml);
            var response = service.RetrieveMultiple(query);

            if (response == null || response.Entities == null || response.Entities.Count == 0)
            {
                trace.Trace("response is null or response.Entities is null or count is null.");
                return null;
            }

            if (response.Entities.Count > 1)
                throw new InvalidPluginExecutionException("Multiple queues exist with the same name.");

            trace.Trace("GetQueueFor - end");
            return new EntityReference(EntityName.Queue, response.Entities[0].Id);

        }

        private Guid GetSourceMarketFrom(CaseDetail caseDetails, ITracingService trace)
        {
            if (caseDetails == null) return Guid.Empty;
            if (trace == null) return Guid.Empty;

            trace.Trace("GetSourceMarketFrom - start");
            if (caseDetails.BookingSourceMarket != Guid.Empty)
            {
                trace.Trace("source market retrieved from booking.");
                return caseDetails.BookingSourceMarket;
            }
            if (caseDetails.CaseSourceMarket != Guid.Empty)
            {
                trace.Trace("source market retrieved from case.");
                return caseDetails.CaseSourceMarket;
            }

            trace.Trace("source market could not be determined");
            return Guid.Empty;
        }

        private int GetDepartmentFrom(Collection<string> userRoles, ITracingService trace)
        {
            if (userRoles == null) return -1;
            if (trace == null) return -1;

            trace.Trace("GetDepartmentFrom - start");
            if (userRoles.Contains("Tc.CustomerRelations.Base"))
            {
                trace.Trace("User is from customer relations.");
                return 1;
            }
            else if (userRoles.Contains("Tc.Ids.Base"))
            {
                trace.Trace("User is from Ids.");
                return 2;
            }

            trace.Trace("Returning -1");
            return -1;
        }

        private Collection<string> GetRolesFor(Guid owner, IOrganizationService service, ITracingService trace)
        {
            if (owner == Guid.Empty) return null;
            if (service == null) return null;
            if (trace == null) return null;

            trace.Trace("GetRolesFor - start");
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                              <entity name='role'>
                                <attribute name='name' />
                                <attribute name='roleid' />
                                <order attribute='name' descending='false' />
                                <link-entity name='systemuserroles' from='roleid' to='roleid' visible='false' intersect='true'>
                                  <link-entity name='systemuser' from='systemuserid' to='systemuserid' alias='aa'>
                                    <filter type='and'>
                                      <condition attribute='systemuserid' operator='eq' value='{DD3A9A52-2DF0-E611-8102-3863BB351D00}' />
                                    </filter>
                                  </link-entity>
                                </link-entity>
                              </entity>
                            </fetch>";
            fetchXml = string.Format(fetchXml, owner.ToString());
            var query = new FetchExpression(fetchXml);

            trace.Trace("Calling retrieve multiple of service.");
            var response = service.RetrieveMultiple(query);

            if (response == null || response.Entities == null || response.Entities.Count == 0)
            {
                trace.Trace("response is null or response.Entities is null or count is null.");
                return null;
            }

            Collection<string> roles = new Collection<string>();

            foreach (var role in response.Entities)
            {
                roles.Add(role[Attributes.Role.Name].ToString());
            }

            trace.Trace("GetRolesFor - end");
            return roles;
        }

        private CaseDetail GetCaseDetailsFor(EntityReference caseId, IOrganizationService service, ITracingService trace)
        {
            if(caseId == null ) return null;
            if (service == null) return null;
            if (trace == null) return null;

            trace.Trace("GetCaseDetailsFor - start");
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='incident'>
                                <attribute name='incidentid' />
                                <attribute name='tc_sourcemarketid' />
                                <attribute name='ownerid' />
                                <filter type='and'>
                                      <condition attribute='incidentid' operator='eq' value='{0}' />
                                    </filter>
                                   < link-entity name='tc_booking' from='tc_bookingid' to='tc_bookingid' visible='false' link-type='outer' alias='a'>
                                  <attribute name='tc_sourcemarketid' />
                                </link-entity>
                              </entity>
                            </fetch>";
            fetchXml = string.Format(fetchXml, caseId);
            var query = new FetchExpression(fetchXml);

            trace.Trace("Calling retrieve multiple of service.");
            var response = service.RetrieveMultiple(query);

            if (response == null || response.Entities == null || response.Entities.Count == 0)
            {
                trace.Trace("response is null or response.Entities is null or count is null");
                return null;
            }
            
            if (response.Entities.Count > 1)
            {
                trace.Trace("Multiple queues exist with the same name.");
                throw new InvalidPluginExecutionException("Multiple queues exist with the same name.");
            }

            var caseDetail = new CaseDetail();
            if (response.Entities[0].Contains(Attributes.Booking.SourceMarketId) && response.Entities[0][Attributes.Booking.SourceMarketId] != null)
                caseDetail.CaseSourceMarket = ((EntityReference)(response.Entities[0][Attributes.Booking.SourceMarketId])).Id;
            if (response.Entities[0].Contains($"a.{Attributes.Booking.SourceMarketId}") && response.Entities[0][$"a.{Attributes.Booking.SourceMarketId}"] !=null)
                caseDetail.BookingSourceMarket = ((EntityReference)((AliasedValue)response.Entities[0][$"a.{Attributes.Booking.SourceMarketId}"]).Value).Id;
            if (response.Entities[0].Contains(Attributes.Booking.Owner) && response.Entities[0][Attributes.Booking.Owner] != null)
                caseDetail.Owner = ((EntityReference)(response.Entities[0][Attributes.Booking.Owner])).Id;


            trace.Trace("GetCaseDetailsFor - end");
            return caseDetail;
        }
    }
}
