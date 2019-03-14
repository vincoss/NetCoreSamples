﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data_OData_Sample.Models
{
    public partial class BusinessEntityContact
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public int PersonId { get; set; }
        public int ContactTypeId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual Person Person { get; set; }
    }
}
