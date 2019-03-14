﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OData_AdventureWorks.Models
{
    public partial class BusinessEntityAddress
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Address Address { get; set; }
        public virtual AddressType AddressType { get; set; }
        public virtual BusinessEntity BusinessEntity { get; set; }
    }
}
