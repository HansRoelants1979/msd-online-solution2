﻿using System;

namespace Tc.Crm.Common.Models
{
    public class AssignInformation
    {
        public Guid RecordId { get; set; }

        public string RecordName { get; set; }

        public string EntityName { get; set; }

        public Owner RecordOwner { get; set; }
    }
}
