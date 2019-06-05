using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WevApps_TagHelpers_V3.Models;

namespace WevApps_TagHelpers_V3
{
    public static class Helper
    {

        public static Person GetPerson(int id)
        {
            var persons = new List<Person>();
            var colours = new List<string>
            { "Red", "Orange", "Yellow"};

            persons.Add(new Person { Age = 0, Colors = colours });
            persons.Add(new Person { Age = 1, Colors = colours });

            return persons.SingleOrDefault(x => x.Age == id);
        }

        public static IEnumerable<ToDoItem> GetTodos()
        {
            return new[] 
            {
                new ToDoItem { Name = "A", IsDone = true},
                new ToDoItem { Name = "B", IsDone = false},
                new ToDoItem { Name = "C", IsDone = true},
            };
        }
    }
}
