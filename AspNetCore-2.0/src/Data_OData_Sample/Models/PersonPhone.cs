using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data_OData_Sample.Models
{
    public partial class PersonPhone
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Person BusinessEntity { get; set; }
        public virtual PhoneNumberType PhoneNumberType { get; set; }
    }
}
