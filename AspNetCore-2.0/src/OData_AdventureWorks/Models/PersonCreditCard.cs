using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OData_AdventureWorks.Models
{
    public partial class PersonCreditCard
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public int CreditCardId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Person BusinessEntity { get; set; }
        public virtual CreditCard CreditCard { get; set; }
    }
}
