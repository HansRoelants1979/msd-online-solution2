﻿using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tc.Crm.Common;
using Tc.Crm.Common.Models;
using Tc.Crm.WebJob.DeallocateResortTeam.Models;

namespace Tc.Crm.WebJob.DeallocateResortTeam.Services
{
    public interface IDeallocationService : IDisposable
    {
        /// <summary>
        /// Get bookings, customers and cases to be reassigned to the default team
        /// </summary>
        /// <param name="bookingDeallocationRequest"> deallocation request</param>
        /// <returns>bookings, customers and cases to be assigned to default team</returns>
        DeallocationExecutionRequest FetchBookingsForDeallocation(DeallocationRequest bookingDeallocationRequest);

        /// <summary>
        /// Assign bookings, customers and cases to default team
        /// </summary>
        /// <param name="request"></param>
        /// <param name="batchSize"></param>
        void DeallocateEntities(DeallocationExecutionRequest request, int batchSize);

        Collection<Guid> GetUsersBySecurityRole(Dictionary<Guid, OwnerType> caseOwnersandDefaultTeams, string securityRole);

        Collection<Guid> GetTeamsBySecurityRole(Dictionary<Guid, OwnerType> caseOwnersandDefaultTeams, string securityRole);

        string GetLookupConditions(IEnumerable<Guid> guids);

        Dictionary<Guid,OwnerType> GetCaseOwnersandDefaultTeams(EntityCollection caseCollection);

        Collection<Guid> GetCollectionByAttribute(EntityCollection entityCollection, string attributeName);

        void CreateUpdateRequests(Collection<Entity> requets, IEnumerable<EntityModel> entities);
        DeallocationExecutionRequest ConvertCrmResponse(EntityCollection collection, Collection<Guid> customerRelationUsers, Collection<Guid> customerRelationTeams);

        string GetNameConditions(string[] names);
    }
}
