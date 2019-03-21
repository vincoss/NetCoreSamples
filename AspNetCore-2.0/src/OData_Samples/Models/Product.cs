﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Models
{
    public class Supplier
    {
        [Key]
        public string Key { get; set; }
        public string Name { get; set; }
    }

    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Rating { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Supplier")]
        public string SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
