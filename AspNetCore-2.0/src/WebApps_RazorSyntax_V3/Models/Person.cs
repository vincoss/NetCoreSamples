using System;


namespace WebApps_RazorSyntax_V3.Models
{
    public class Person
    {
        public  Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; set; }
        public int Age { get; set; }
    }
}
