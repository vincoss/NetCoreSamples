using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorials_TodoApi_V3.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
