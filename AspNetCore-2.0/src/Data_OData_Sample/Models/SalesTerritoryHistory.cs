using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data_OData_Sample.Models
{
    public partial class SalesTerritoryHistory
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public int TerritoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual SalesPerson BusinessEntity { get; set; }
        public virtual SalesTerritory Territory { get; set; }
    }
}
