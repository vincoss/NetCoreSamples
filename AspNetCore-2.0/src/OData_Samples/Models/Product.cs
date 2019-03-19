using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Supplier
    {
        [Key]
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
