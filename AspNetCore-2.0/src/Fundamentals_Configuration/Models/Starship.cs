using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_Configuration.Models
{
    public class Starship
    {
        public string Name { get; set; }
        public string Registry { get; set; }
        public string Class { get; set; }
        public decimal Length { get; set; }
        public bool Commissioned { get; set; }
    }

    public class TvShow
    {
        public Metadata Metadata { get; set; }
        public Actors Actors { get; set; }
        public string Legal { get; set; }
    }

    public class Metadata
    {
        public string Series { get; set; }
        public string Title { get; set; }
        public DateTime AirDate { get; set; }
        public int Episodes { get; set; }
    }

    public class Actors
    {
        public string Names { get; set; }
    }

    public class ArrayExample
    {
        public string[] Entries { get; set; }
    }

    public class JsonArrayExample
    {
        public string Key { get; set; }
        public string[] Subsection { get; set; }
    }
}
