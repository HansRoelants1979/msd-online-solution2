﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Crm.Common.Models
{
    public sealed class Customer : EntityModel
    {
        public CustomerType CustomerType { get; set; }
        public override string EntityName
        {
            get
            {
                return CustomerType == CustomerType.Account ? Constants.EntityName.Account : Constants.EntityName.Contact;
            }
        }
    }
}
