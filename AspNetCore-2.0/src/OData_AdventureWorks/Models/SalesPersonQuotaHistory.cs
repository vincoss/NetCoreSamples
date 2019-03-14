using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OData_AdventureWorks.Models
{
    public partial class SalesPersonQuotaHistory
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public DateTime QuotaDate { get; set; }
        public decimal SalesQuota { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual SalesPerson BusinessEntity { get; set; }
    }
}
