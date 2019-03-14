using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data_OData_Sample.Models
{
    public partial class EmployeePayHistory
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public DateTime RateChangeDate { get; set; }
        public decimal Rate { get; set; }
        public byte PayFrequency { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Employee BusinessEntity { get; set; }
    }
}
