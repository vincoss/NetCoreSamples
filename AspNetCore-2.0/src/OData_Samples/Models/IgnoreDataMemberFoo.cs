using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OData_Samples.Models
{
    public class IgnoreDataMemberFoo
    {
        public string Name { get; set; }

        [IgnoreDataMember] // Do not show this in query
        public decimal Salary { get; set; }

        public decimal Age { get; set; }
    }
}
