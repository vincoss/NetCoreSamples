using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OData_AdventureWorks.Models
{
    public enum PhoneNumberTypeEnum
    {
        Cell = 1,
        Home = 2,
        Work = 3
    }


    public class EntityWithEnum
    {

        public string Description { get; set; }
        public PhoneNumberTypeEnum PhoneNumberType { get; set; }

        [Key]
        public string Name { get; set; }
    }
}
