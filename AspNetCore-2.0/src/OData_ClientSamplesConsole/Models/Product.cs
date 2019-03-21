using System.Collections.Generic;


namespace OData_ClientSamplesConsole.Models
{
    public class Supplier
    {
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
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public string SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
