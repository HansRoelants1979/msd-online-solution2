﻿using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Query;

namespace Tc.Crm.CustomWorkflowSteps
{
    public class CommonXrm
    {
              
        
        /// <summary>
        /// Call this method to create or update record
        /// </summary>
        /// <param name="entityRecord">Entity to Create or Update</param>
        /// <returns></returns>
        public SuccessMessage UpsertEntity(Entity entityRecord, IOrganizationService service)
        {  

            SuccessMessage successMsg = null;
            if (service != null)
            {
                UpsertRequest request = new UpsertRequest()
                {
                    Target = entityRecord
                };

                try
                {

                    // Execute UpsertRequest and obtain UpsertResponse. 
                    UpsertResponse response = (UpsertResponse)service.Execute(request);
                    if (response.RecordCreated)
                        successMsg = new SuccessMessage
                        {
                            Id = response.Target.Id.ToString(),
                            EntityName = entityRecord.LogicalName,
                            Create = true
                           
                        };
                    else
                        successMsg = new SuccessMessage()
                        {
                            Id = response.Target.Id.ToString(),
                            EntityName = entityRecord.LogicalName,
                            Create = false
                        };


                }

                // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
                catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
                {
                    throw;
                }
            }

           
            return successMsg;
        }


        /// <summary>
        /// Call this method for bulk create
        /// </summary>
        /// <param name="entities"></param>       
        /// <returns></returns>
        public List<SuccessMessage> BulkCreate(EntityCollection entities, IOrganizationService service)
        {
            List<SuccessMessage> successMsg = null;
            if (service != null && entities != null && entities.Entities.Count > 0)
            {
                var requestWithResults = new ExecuteMultipleRequest()
                {
                    // Assign settings that define execution behavior: continue on error, return responses. 
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = false,
                        ReturnResponses = true
                    },
                    // Create an empty organization request collection.
                    Requests = new OrganizationRequestCollection()
                };


                // Add a CreateRequest for each entity to the request collection.
                foreach (var entity in entities.Entities)
                {
                    CreateRequest createRequest = new CreateRequest { Target = entity };
                    requestWithResults.Requests.Add(createRequest);
                }

                try
                {
                    // Execute all the requests in the request collection using a single web method call.
                    ExecuteMultipleResponse responseWithResults =
                        (ExecuteMultipleResponse)service.Execute(requestWithResults);

                    successMsg = new List<SuccessMessage>();

                    // Get the results returned in the responses.
                    foreach (var responseItem in responseWithResults.Responses)
                    {
                        // A valid response.
                        if (responseItem.Response != null)
                        {
                            var msg = new SuccessMessage
                            {
                                Id = requestWithResults.Requests[responseItem.RequestIndex].RequestId.Value.ToString(),
                                EntityName = requestWithResults.Requests[responseItem.RequestIndex].RequestName,
                                Key = requestWithResults.Requests[responseItem.RequestIndex].Parameters[""].ToString()
                              
                            };
                            successMsg.Add(msg);
                        }
                        // An error has occurred.
                        else if (responseItem.Fault != null)
                        {

                        }
                    }
                }
                // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
                catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
                {
                    throw;
                }

            }

            return successMsg;
        }


        /// <summary>
        /// Call this method for bulk delete
        /// </summary>      
        /// <param name="entityReferences">Collection of EntityReferences to Delete</param>
        public void BulkDelete(DataCollection<EntityReference> entityReferences, IOrganizationService service)
        {
            List<SuccessMessage> successMsg = null;
            if (service != null && entityReferences != null && entityReferences.Count > 0)
            {
                // Create an ExecuteMultipleRequest object.
                var requestWithResults = new ExecuteMultipleRequest()
                {
                    // Assign settings that define execution behavior: continue on error, return responses. 
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = false,
                        ReturnResponses = true
                    },
                    // Create an empty organization request collection.
                    Requests = new OrganizationRequestCollection()
                };

                // Add a DeleteRequest for each entity to the request collection.
                foreach (var entityRef in entityReferences)
                {
                    DeleteRequest deleteRequest = new DeleteRequest { Target = entityRef };
                    requestWithResults.Requests.Add(deleteRequest);
                }

                try {
                    // Execute all the requests in the request collection using a single web method call.
                    ExecuteMultipleResponse responseWithResults = (ExecuteMultipleResponse)service.Execute(requestWithResults);

                    // Get the results returned in the responses.
                    foreach (var responseItem in responseWithResults.Responses)
                    {
                        // A valid response.
                        if (responseItem.Response != null)
                        {
                            var msg = new SuccessMessage
                            {
                                Id = requestWithResults.Requests[responseItem.RequestIndex].RequestId.Value.ToString(),
                                EntityName = requestWithResults.Requests[responseItem.RequestIndex].RequestName
                              
                            };
                            successMsg.Add(msg);
                        }
                        // An error has occurred.
                        else if (responseItem.Fault != null)
                        {

                        }
                    }
                }
                // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
                catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
                {
                    throw;
                }
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logicalName"></param>
        /// <param name="alternateKey"></param>
        /// <param name="alternateKeyValue"></param>
        /// <returns></returns>
        public EntityReference SetLookupValueUsingAlternateKey(string logicalName, string alternateKey, string alternateKeyValue, IOrganizationService service)
        {
            EntityReference entRef = null;
            QueryByAttribute querybyexpression = new QueryByAttribute(logicalName);
            querybyexpression.ColumnSet = new ColumnSet(logicalName + "id");           
            querybyexpression.Attributes.AddRange(alternateKey);           
            querybyexpression.Values.AddRange(alternateKeyValue);           
            EntityCollection retrieved = service.RetrieveMultiple(querybyexpression);
            if (retrieved != null && retrieved.Entities.Count == 1)
            {
                entRef = new EntityReference(logicalName, retrieved.Entities[0].Id);
            }
            return entRef;
        }

        public EntityCollection RetrieveMultipleRecords(string entityName, string[] columns, string[] filterKeys, string[] filterValues, IOrganizationService service)
        {
            var query = new QueryExpression(entityName);
            query.ColumnSet = new ColumnSet(columns);
            FilterExpression fltrExpr = null;
            for (int i = 0; i < filterKeys.Length; i++)
            {
                ConditionExpression condExpr = new ConditionExpression();
                condExpr.AttributeName = filterKeys[i];
                condExpr.Operator = ConditionOperator.Equal;
                condExpr.Values.Add(filterValues[i]);

                fltrExpr = new FilterExpression(LogicalOperator.And);
                fltrExpr.AddCondition(condExpr);

                query.Criteria.AddFilter(fltrExpr);
            }
           return GetRecordsUsingQuery(query, service);

        }

        EntityCollection GetRecordsUsingQuery(QueryExpression queryExpr, IOrganizationService service)
        {
            EntityCollection entCollection = null;
            entCollection = service.RetrieveMultiple(queryExpr);
            return entCollection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptionSetValue SetOptionSetValue(int value)
        {
            OptionSetValue optionValue = new OptionSetValue(value);
            return optionValue;
        }



    }


    public class SuccessMessage
    {
        public bool Create { get; set; }
        public string EntityName { get; set; }
        public string Id { get; set; }
        public string Details { get; set; }
        public string Key { get; set; }       

    }




}
