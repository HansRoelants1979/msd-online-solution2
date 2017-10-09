﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System.IO;
using System.Text;
using System.Globalization;
using System.Xml;
using System;
using Tc.Crm.CustomWorkflowSteps.ProcessBooking.Models;
using System.ServiceModel;

namespace Tc.Crm.CustomWorkflowSteps
{
    public static class CommonXrm
    {


        /// <summary>
        /// To delete records by filtering the data
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="columns"></param>
        /// <param name="filterKeys"></param>
        /// <param name="filterValues"></param>
        /// <param name="service"></param>
        public static void MarkEntityRecordsAsPendingDelete(string entityName, string[] columns, string[] filterKeys, string[] filterValues, IOrganizationService service)
        {
            if (service == null) return;
            EntityCollection entityCollection = CommonXrm.RetrieveMultipleRecords(entityName, columns, filterKeys, filterValues, service);
            foreach (var item in entityCollection.Entities)
            {
                var entityToMarkAsPendingDelete = new Entity(entityName);
                entityToMarkAsPendingDelete.Id = item.Id;
                entityToMarkAsPendingDelete[Attributes.Booking.StateCode] = new OptionSetValue((int)Statecode.InActive);
                if (entityName == EntityName.BookingAccommodation)
                    entityToMarkAsPendingDelete[Attributes.Booking.StatusCode] = new OptionSetValue(950000007);
                else if (entityName == EntityName.BookingTransfer)
                    entityToMarkAsPendingDelete[Attributes.Booking.StatusCode] = new OptionSetValue(950000000);
                else if (entityName == EntityName.BookingTransport)
                    entityToMarkAsPendingDelete[Attributes.Booking.StatusCode] = new OptionSetValue(950000000);
                else if (entityName == EntityName.BookingExtraService)
                    entityToMarkAsPendingDelete[Attributes.Booking.StatusCode] = new OptionSetValue(950000001);
                else if (entityName == EntityName.CustomerBookingRole)
                    entityToMarkAsPendingDelete[Attributes.Booking.StatusCode] = new OptionSetValue(950000000);

                service.Update(entityToMarkAsPendingDelete);
            }
        }

        #region Option set mappings
        public static OptionSetValue GetCommunity(string text)
        {
            int value = -1;
            switch (text)
            {
                case "Facebook":
                    value = 1;
                    break;
                case "Google":
                    value = 0;
                    break;
                case "Twitter":
                    value = 2;
                    break;
                case "Other":
                    value = 0;
                    break;
                case "":
                case null:
                    value = 0;
                    break;
                default:
                    value = 0;
                    break;
            }

            return new OptionSetValue(value);
        }
        public static OptionSetValue GetExternalServiceCode(ExternalServiceCode externalServiceCode)
        {
            int value = -1;
            switch (externalServiceCode)
            {
                case ExternalServiceCode.NotSpecified:
                    value = 950000000;
                    break;
                case ExternalServiceCode.BedBank:
                    value = 950000001;
                    break;
                case ExternalServiceCode.GTA:
                    value = 950000002;
                    break;
                case ExternalServiceCode.HBSI:
                    value = 950000003;
                    break;
                case ExternalServiceCode.Hotel4You:
                    value = 950000004;
                    break;
                case ExternalServiceCode.IberostarBedBank:
                    value = 950000005;
                    break;
                case ExternalServiceCode.Juniper:
                    value = 950000007;
                    break;
                case ExternalServiceCode.SunHotels:
                    value = 950000008;
                    break;
                case ExternalServiceCode.Unknown:
                    value = 950000000;
                    break;
                default:
                    value = 950000000;
                    break;
            }
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetExtraServiceType(ExtraServiceType extraServiceType)
        {
            int value = -1;
            switch (extraServiceType)
            {
                case ExtraServiceType.NotSpecified:
                    value = 950000004;
                    break;
                case ExtraServiceType.CarHire:
                    value = 950000000;
                    break;
                case ExtraServiceType.Insurance:
                    value = 950000001;
                    break;
                case ExtraServiceType.Miscellaneous:
                    value = 950000002;
                    break;
                case ExtraServiceType.OpInsurance:
                    value = 950000003;
                    break;                
                default:
                    value = 950000004;
                    break;
            }
            return new OptionSetValue(value);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public static OptionSetValue GetBoardType(BoardType boardType)
        {
            int value = -1;
            switch (boardType)
            {
                case BoardType.NotSpecified:
                    value = 950000003;
                    break;
                case BoardType.AllInclusive:
                    value = 950000000;
                    break;
                case BoardType.AllInclusivePlus:
                    value = 950000028;
                    break;
                case BoardType.AmericanBreakfast:
                    value = 950000004;
                    break;
                case BoardType.BedAndBreakfast:
                    value = 950000005;
                    break;
                case BoardType.BedEnglishBfast:
                    value = 950000006;
                    break;
                case BoardType.BoardAccordingToDescription:
                    value = 950000007;
                    break;
                case BoardType.Breakfast:
                    value = 950000008;
                    break;
                case BoardType.CateredChalet:
                    value = 950000009;
                    break;
                case BoardType.ClubBoard:
                    value = 950000010;
                    break;
                case BoardType.ContinentalBfast:
                    value = 950000011;
                    break;
                case BoardType.CruiseBoard:
                    value = 950000012;
                    break;
                case BoardType.DeluxeHalfBoard:
                    value = 950000013;
                    break;
                case BoardType.DineOut:
                    value = 950000014;
                    break;
                case BoardType.DrinksInclusive:
                    value = 950000015;
                    break;
                case BoardType.EveningMeal:
                    value = 950000016;
                    break;
                case BoardType.FlyDrive:
                    value = 950000017;
                    break;
                case BoardType.FullBoard:
                    value = 950000001;
                    break;
                case BoardType.FullBoardPlus:
                    value = 950000018;
                    break;
                case BoardType.HalfBoard:
                    value = 950000002;
                    break;
                case BoardType.HalfBoardUpgrade:
                    value = 950000020;
                    break;
                case BoardType.MealPlan:
                    value = 950000021;
                    break;
                case BoardType.NotApplicable:
                    value = 950000022;
                    break;
                case BoardType.RoomOnly:
                    value = 950000023;
                    break;
                case BoardType.Unknown:
                    value = 950000003;
                    break;
                case BoardType.ValueDiningPlan:
                    value = 950000025;
                    break;
                case BoardType.VariableBoard:
                    value = 950000026;
                    break;
                case BoardType.WithoutAny:
                    value = 950000027;
                    break;
                default:
                    value = 950000003;
                    break;
            }
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetTransferServiceLevel(TransferServiceLevel transferServiceLevel)
        {
            int value = -1;
            switch (transferServiceLevel)
            {
                case TransferServiceLevel.NotSpecified:
                    value = 950000000;
                    break;
                case TransferServiceLevel.Differentiated:
                    value = 950000001;
                    break;
                case TransferServiceLevel.RegularComplementary:
                    value = 950000002;
                    break;
                default:
                    value = 950000000;
                    break;
            }
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetAccommodationStatus(AccommodationStatus accommodationStatus)
        {
            int value = -1;
            switch (accommodationStatus)
            {
                case AccommodationStatus.NotSpecified:
                    value = 1;
                    break;
                case AccommodationStatus.OK:
                    value = 950000002;
                    break;
                case AccommodationStatus.Request:
                    value = 950000003;
                    break;
                case AccommodationStatus.PartialRequest:
                    value = 950000004;
                    break;
                default:
                    value = 1;
                    break;

            }

            return new OptionSetValue(value);
        }
        public static OptionSetValue GetAccommodationType(AccommodationType accommodationType)
        {
            int value = -1;
            switch (accommodationType)
            {
                case AccommodationType.NotSpecified:
                    value = 950000000;
                    break;
                case AccommodationType.Cruise:
                    value = 950000001;
                    break;
                case AccommodationType.Hotel:
                    value = 950000002;
                    break;
                case AccommodationType.Accommodation:
                    value = 950000003;
                    break;
                default:
                    value = 950000000;
                    break;

            }
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetCustomerStatus(CustomerStatus customerStatus)
        {
            int value = -1;
            switch (customerStatus)
            {
                case CustomerStatus.NotSpecified:
                    value = 950000002;
                    break;
                case CustomerStatus.Active:
                    value = 1;
                    break;
                case CustomerStatus.Blacklisted:
                    value = 950000003;
                    break;
                case CustomerStatus.Deceased:
                    value = 950000004;
                    break;
                case CustomerStatus.Inactive:
                    value = 950000005;
                    break;
                default:
                    value = 950000002;
                    break;
            }
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetGender(Gender gender)
        {
            int value = -1;
            switch (gender)
            {
                case Gender.NotSpecified:
                    value = 950000002;
                    break;
                case Gender.Male:
                    value = 950000000;
                    break;
                case Gender.Female:
                    value = 950000001;
                    break;
                default:
                    value = 950000002;
                    break;
            }
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetEmailType(EmailType emailType)
        {
            int value = -1;
            switch (emailType)
            {
                case EmailType.NotSpecified:
                    value = 950000002;
                    break;

                case EmailType.Primary:
                    value = 950000000;

                    break;
                case EmailType.Promo:
                    value = 950000001;
                    break;
                default:
                    value = 950000002;
                    break;
            }
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetBookingStatus(BookingStatus bookingStatus)
        {
            int value = -1;
            switch (bookingStatus)
            {
                case BookingStatus.NotSpecified:
                    value = 1;
                    break;
                case BookingStatus.Booked:
                    value = 950000002;
                    break;
                case BookingStatus.Cancelled:
                    value = 950000003;
                    break;
                default:
                    value = 1;
                    break;
            }

            return new OptionSetValue(value);
        }
        public static OptionSetValue GetPhoneType(PhoneType phoneType)
        {
            int value = -1;
            switch (phoneType)
            {
                case PhoneType.NotSpecified:
                    value = 950000002;
                    break;
                case PhoneType.Home:
                    value = 950000001;
                    break;
                case PhoneType.Mobile:
                    value = 950000000;
                    break;
                case PhoneType.Business:
                    value = 950000003;
                    break;
                default:
                    value = 950000002;
                    break;

            }
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetTransferType(TransferType transferType)
        {
            int value = -1;
            switch (transferType)
            {
                case TransferType.NotSpecified:
                    value = 950000003;
                    break;
                case TransferType.Inbound:
                    value = 950000000;
                    break;
                case TransferType.Outbound:
                    value = 950000001;
                    break;
                case TransferType.TransferBetweenHotels:
                    value = 950000002;
                    break;
                default:
                    value = 950000003;
                    break;
            }

            return new OptionSetValue(value);
        }
        public static OptionSetValue GetTransportType(TransportType transportType)
        {
            int value = -1;
            switch (transportType)
            {
                case TransportType.NotSpecified:
                    value = 950000006;
                    break;
                case TransportType.Coach:
                    value = 950000000;
                    break;
                case TransportType.CharterFlight:
                    value = 950000001;
                    break;
                case TransportType.ScheduledFlight:
                    value = 950000002;
                    break;
                case TransportType.Ferry:
                    value = 950000003;
                    break;
                case TransportType.Motorail:
                    value = 950000004;
                    break;
                case TransportType.Rail:
                    value = 950000005;
                    break;
                default:
                    value = 950000006;
                    break;
            }

            return new OptionSetValue(value);
        }
        public static OptionSetValue GetSegment(string text)
        {
            int value = -1;
            switch (text)
            {
                case "1":
                    value = 950000000;
                    break;
                case "2":
                    value = 950000001;
                    break;
                case "3":
                    value = 950000002;
                    break;
                case "4":
                    value = 950000003;
                    break;
                case "5":
                    value = 950000004;
                    break;
                case "":
                case null:
                    value = 950000005;
                    break;
                default:
                    value = 950000005;
                    break;
            }

            return new OptionSetValue(value);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public static OptionSetValue GetSalutation(string text)
        {
            int value = -1;
            switch (text)
            {
                case "Mr":
                    value = 950000000;
                    break;
                case "Mrs":
                    value = 950000001;
                    break;
                case "Ms":
                    value = 950000002;
                    break;
                case "Miss":
                    value = 950000003;
                    break;
                case "Dr":
                    value = 950000004;
                    break;
                case "Sir":
                    value = 950000005;
                    break;
                case "Prof.":
                    value = 950000006;
                    break;
                case "Lord":
                    value = 950000007;
                    break;
                case "Lady":
                    value = 950000008;
                    break;
                case "":
                case null:
                    value = -1;
                    break;
                default:
                    value = -1;
                    break;
            }
            if (value == -1) return null;
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetLanguage(string text)
        {
            int value = -1;
            switch (text)
            {
                case "EN":
                    value = 950000000;
                    break;
                case "DE":
                    value = 950000001;
                    break;
                case "NL":
                    value = 950000002;
                    break;
                case "FR":
                    value = 950000003;
                    break;
                case "ES":
                    value = 950000004;
                    break;
                case "DA":
                    value = 950000005;
                    break;
                case "":
                case null:
                    value = 950000006;
                    break;
                default:
                    value = 950000006;
                    break;
            }

            return new OptionSetValue(value);
        }

        public static OptionSetValue GetPreferredContactMethodCode(string preferredContactMethod)
        {
            int value = -1;
            switch (preferredContactMethod)
            {                
                case "Email":
                    value = 2;
                    break;
                case "Phone":
                    value = 3;
                    break;
                case "Mail":
                    value = 5;
                    break;
                default:
                    value = 1;
                    break;
            }
            return new OptionSetValue(value);
        }    
            
        public static OptionSetValue GetMarketingByEmail(string marketingByEmail)
        {
            int value = -1;
            switch (marketingByEmail)
            {
                case "true":
                    value = 950000000;
                    break;
                case "false":
                    value = 950000001;
                    break;
            }
            return new OptionSetValue(value);
        }

        public static OptionSetValue GetMarketingByPost(string marketingByPost)
        {
            int value = -1;
            switch (marketingByPost)
            {
                case "true":
                    value = 950000000;
                    break;
                case "false":
                    value = 950000001;
                    break;
            }
            return new OptionSetValue(value);
        }

        public static OptionSetValue GetMarketingByPhone(string marketingByPhone)
        {
            int value = -1;
            switch (marketingByPhone)
            {
                case "true":
                    value = 950000000;
                    break;
                case "false":
                    value = 950000001;
                    break;
            }
            return new OptionSetValue(value);
        }

        public static OptionSetValue GetMarketingBySms(string marketingBySms)
        {
            int value = -1;
            switch (marketingBySms)
            {
                case "true":
                    value = 950000000;
                    break;
                case "false":
                    value = 950000001;
                    break;
            }
            return new OptionSetValue(value);
        }

        public static OptionSetValue GetMarketingConsent(string marketingConsent)
        {
            int value = -1;
            switch (marketingConsent)
            {               
                case "true":
                    value = 950000000;
                    break;
                case "false":
                    value = 950000001;
                    break;               
            }
            return new OptionSetValue(value);
        }
        public static OptionSetValue GetProductTypeCode(ProductTypeCode productTypeCode)
        {
            int value = -1;
            switch (productTypeCode)
            {
                case ProductTypeCode.NotSpecified:
                    value = 950000000;
                    break;
                case ProductTypeCode.CC:
                    value = 950000001;
                    break;
                case ProductTypeCode.CE:
                    value = 950000002;
                    break;
                case ProductTypeCode.CH:
                    value = 950000003;
                    break;
                case ProductTypeCode.CO:
                    value = 950000004;
                    break;
                case ProductTypeCode.FC:
                    value = 950000005;
                    break;
                case ProductTypeCode.FY:
                    value = 950000006;
                    break;
                case ProductTypeCode.HR:
                    value = 950000007;
                    break;
                case ProductTypeCode.IN:
                    value = 950000008;
                    break;
                case ProductTypeCode.MS:
                    value = 950000009;
                    break;
                case ProductTypeCode.PH:
                    value = 950000010;
                    break;
                case ProductTypeCode.RL:
                    value = 950000011;
                    break;
                case ProductTypeCode.SP:
                    value = 950000012;
                    break;
                case ProductTypeCode.UP:
                    value = 950000013;
                    break;
                default:
                    value = 950000000;
                    break;
            }
            return new OptionSetValue(value);
        }

        public static OptionSetValue GetSourceSystem(SourceSystem sourceSystem)
        {
            int value = -1;
            switch (sourceSystem)
            {
                case SourceSystem.OnTour:
                    value = 950000000;
                    break;
                case SourceSystem.TCV:
                    value = 950000001;
                    break;                
            }
            return new OptionSetValue(value);
        }

        #endregion Option set mappings
        /// <summary>
        /// Call this method to create or update record
        /// </summary>
        /// <param name="entityRecord"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Upsert")]
        public static XrmResponse UpsertEntity(Entity entityRecord, IOrganizationService service)
        {
            if (entityRecord == null) return null;
            if (service == null) return null;

            XrmResponse xrmResponse = null;
            if (service != null)
            {
                UpsertRequest request = new UpsertRequest()
                {
                    Target = entityRecord
                };



                // Execute UpsertRequest and obtain UpsertResponse. 
                UpsertResponse response = (UpsertResponse)service.Execute(request);
                if (response.RecordCreated)
                    xrmResponse = new XrmResponse
                    {
                        Id = response.Target.Id.ToString(),
                        EntityName = entityRecord.LogicalName,
                        Create = true

                    };
                else
                    xrmResponse = new XrmResponse()
                    {
                        Id = response.Target.Id.ToString(),
                        EntityName = entityRecord.LogicalName,
                        Create = false
                    };


            }


            return xrmResponse;
        }

        public static XrmResponse CreateEntity(Entity entityRecord, IOrganizationService service)
        {
            if (entityRecord == null) return null;
            if (service == null) return null;
            
            var id = service.Create(entityRecord);
            return new XrmResponse { Create = true, Id = id.ToString(), EntityName = entityRecord.LogicalName };
        }

        public static XrmResponse UpdateEntity(Entity entityRecord, IOrganizationService service)
        {
            if (entityRecord == null) return null;
            if (service == null) return null;

            service.Update(entityRecord);
            return new XrmResponse { Create = false, EntityName = entityRecord.LogicalName };
        }

        /// <summary>
        /// Call this method for bulk create
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public static void BulkCreate(EntityCollection entities, IOrganizationService service)
        {
            if (entities == null || entities.Entities.Count == 0) return;
            if (service == null) return;

            foreach (var entity in entities.Entities)
            {
                var createRequest = new CreateRequest { Target = entity };
                service.Execute(createRequest);
            }

        }

        public static XrmUpdateResponse BulkUpdate(ExecuteTransactionRequest multipleRequest, IOrganizationService service)
        {           
            if (multipleRequest == null && multipleRequest.Requests == null && multipleRequest.Requests.Count == 0) return null;
            if (service == null) return null;
            ExecuteTransactionResponse multipleResponses = (ExecuteTransactionResponse)service.Execute(multipleRequest);
            return new XrmUpdateResponse { Updated = true, Existing = true };                 
        }

        /// <summary>
        /// To create record
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="service"></param>
        public static void CreateRecord(Entity entity, IOrganizationService service)
        {
            if (entity == null) return;
            if (service == null) return;
            
            var createRequest = new CreateRequest { Target = entity };
            service.Execute(createRequest);           
        }

        
        /// <summary>
        /// To get records by using filter keys and values
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="columns"></param>
        /// <param name="filterKeys"></param>
        /// <param name="filterValues"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public static EntityCollection RetrieveMultipleRecords(string entityName, string[] columns, string[] filterKeys, string[] filterValues, IOrganizationService service)
        {
            if (string.IsNullOrWhiteSpace(entityName)) return null;
            if (service == null) return null;

            var query = new QueryExpression(entityName);
            if (columns == null || columns.Length == 0)
                query.ColumnSet = new ColumnSet(true);
            else
                query.ColumnSet = new ColumnSet(columns);

            if (filterKeys != null && filterValues != null)
            {
                for (int i = 0; i < filterKeys.Length; i++)
                {
                    if (filterValues[i] == null) continue;
                    var condExpr = new ConditionExpression();
                    condExpr.AttributeName = filterKeys[i];
                    condExpr.Operator = ConditionOperator.Equal;
                    condExpr.Values.Add(filterValues[i]);

                    var fltrExpr = new FilterExpression(LogicalOperator.And);
                    fltrExpr.AddCondition(condExpr);

                    query.Criteria.AddFilter(fltrExpr);
                }
            }

            return GetRecordsUsingQuery(query, service);

        }

        /// <summary>
        /// To get records using QueryExpression
        /// </summary>
        /// <param name="queryExpr"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        static EntityCollection GetRecordsUsingQuery(QueryExpression queryExpr, IOrganizationService service)
        {
            EntityCollection entityCollection = null;
            entityCollection = service.RetrieveMultiple(queryExpr);
            return entityCollection;
        }

        public static EntityCollection RetrieveMultipleRecordsFetchXml(string query, IOrganizationService service)
        {
            if (string.IsNullOrWhiteSpace(query)) return null;
            if (service == null) return null;
            EntityCollection entityCollection = new EntityCollection();

            int fetchCount = 10000;
            int pageNumber = 1;
            string pagingCookie = null;

            while (true)
            {
                string xml = CreateXml(query, pagingCookie, pageNumber, fetchCount);
                FetchExpression fetch = new FetchExpression(xml);
                EntityCollection returnCollection = service.RetrieveMultiple(fetch);
                entityCollection.Entities.AddRange(returnCollection.Entities);
                if (returnCollection.MoreRecords)
                {
                    pageNumber++;
                }
                else
                {
                    break;
                }
            }

            return entityCollection;

        }


        public static string CreateXml(string xml, string cookie, int page, int count)
        {
            using (var reader = new XmlTextReader(new StringReader(xml)))
            {
                // Load document
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                return CreateXml(doc, cookie, page, count);
            }
        }


        public static string CreateXml(XmlDocument doc, string cookie, int page, int count)
        {

            if (doc == null) throw new ArgumentNullException("doc");
            XmlAttributeCollection attrs = doc.DocumentElement.Attributes;

            if (cookie != null)
            {
                XmlAttribute pagingAttr = doc.CreateAttribute("paging-cookie");
                pagingAttr.Value = cookie;
                attrs.Append(pagingAttr);
            }

            XmlAttribute pageAttr = doc.CreateAttribute("page");
            pageAttr.Value = System.Convert.ToString(page, CultureInfo.CurrentCulture);
            attrs.Append(pageAttr);

            XmlAttribute countAttr = doc.CreateAttribute("count");
            countAttr.Value = System.Convert.ToString(count, CultureInfo.CurrentCulture);
            attrs.Append(countAttr);

            StringBuilder sb = new StringBuilder(1024);
            using (var stringWriter = new StringWriter(sb, CultureInfo.CurrentCulture))
            {
                XmlTextWriter writer = new XmlTextWriter(stringWriter);
                doc.WriteTo(writer);
                return sb.ToString();
            }
        }

    }

    public class XrmResponse
    {
        public bool Create { get; set; }
        public string EntityName { get; set; }
        public string Id { get; set; }
        public string Details { get; set; }
        public string Key { get; set; }

    }

    public class XrmUpdateResponse :XrmResponse
    {
        public bool Existing { get; set; }
        public bool Updated { get; set; }
    }

}
