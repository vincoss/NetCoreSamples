using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data_OData_Sample.Models
{
    public partial class Store
    {
        public Store()
        {
            Customer = new HashSet<Customer>();
        }

        [Key]
        public int BusinessEntityId { get; set; }
        public string Name { get; set; }
        public int? SalesPersonId { get; set; }
        public string Demographics { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }
        public virtual SalesPerson SalesPerson { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
    }
}
