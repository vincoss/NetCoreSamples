using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data_OData_Sample.Models
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
