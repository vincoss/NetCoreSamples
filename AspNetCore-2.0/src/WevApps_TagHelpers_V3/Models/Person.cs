using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WevApps_TagHelpers_V3.Models
{
    public class Person
    {
        public Person()
        {
            Colors = new List<string>();
        }

        public List<string> Colors { get; set; }

        public int Age { get; set; }
    }
}
