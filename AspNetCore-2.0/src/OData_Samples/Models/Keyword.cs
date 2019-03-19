using System;


namespace OData_Samples.Models
{
    public class Keyword
    {
        //public int KeyworkdId { get; set; } // OR in GetEdmModel .HasKey(x => x.Name)
        public string Name { get; set; }    // Read only
        public decimal Weight { get; set; }  // Read only
        public DateTime ReleaseDate { get; set; }
    }

    public class Variable
    {
        public string Name { get; set; }
        public Keyword Keyword { get; set; }
    }
}
