﻿using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using Tc.Crm.WebJob.AllocateResortTeam.Models;
using Tc.Crm.Common;
using Tc.Crm.Common.Models;

namespace Tc.Crm.WebJob.AllocateResortTeam.Services
{
    public interface IAllocationService : IDisposable
    {
        IList<BookingAllocationResponse> GetBookingAllocations(BookingAllocationRequest bookingAllocationRequest);
        IList<BookingAllocationResponse> PrepareBookingAllocation(EntityCollection bookingCollection);
        StringBuilder GetDestinationGateways(IList<Guid> destinationGateways);
        void ProcessBookingAllocations(IList<BookingAllocationResortTeamRequest> bookingAllocationResortTeamRequest);
        OwnerType GetOwnerType(EntityReference owner);
        Owner GetOwner(Entity entity, string attribute, bool isAliasedValue);
    }
}