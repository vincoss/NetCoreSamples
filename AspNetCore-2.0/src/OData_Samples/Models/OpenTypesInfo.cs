using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Models
{
    public class OpenTypesInfo { }

    public enum CategoryOt
    {
        Book,
        Magazine,
        EBook
    }

    public class AddressOt
    {
        public string City { get; set; }
        public string Street { get; set; }
    }

    public class CustomerOt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressOt Address { get; set; }
    }

    public class PressOt
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public CategoryOt Category { get; set; }
        public IDictionary<string, object> DynamicProperties { get; set; }
    }

    public class BookOt
    {
        [Key]
        public string ISBN { get; set; }
        public string Title { get; set; }
        public PressOt Press { get; set; }
        public IDictionary<string, object> Properties { get; set; }
    }
}
