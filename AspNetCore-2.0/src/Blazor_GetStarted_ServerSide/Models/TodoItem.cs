using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_ServerSide.Models
{
    public class TodoItem
    {
        public TodoItem()
        {
            IsDone = true;
        }

        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
