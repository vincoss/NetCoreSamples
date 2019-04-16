using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_Options.Models
{
    public class MyOptions
    {
        public MyOptions()
        {
            // Set default value.
            Option1 = "value1_from_ctor";
        }

        public string Option1 { get; set; }
        public int Option2 { get; set; } = 5;
    }

    public class MyOptionsWithDelegateConfig
    {
        public MyOptionsWithDelegateConfig()
        {
            // Set default value.
            Option1 = "value1_from_ctor";
        }

        public string Option1 { get; set; }
        public int Option2 { get; set; } = 5;
    }

    public class MySubOptions
    {
        public MySubOptions()
        {
            // Set default values.
            SubOption1 = "value1_from_ctor";
            SubOption2 = 5;
        }

        public string SubOption1 { get; set; }
        public int SubOption2 { get; set; }
    }

    public class AnnotatedOptions
    {
        [Required]
        public string Required { get; set; }

        [StringLength(5, ErrorMessage = "Too long.")]
        public string StringLength { get; set; }

        [Range(-5, 5, ErrorMessage = "Out of range.")]
        public int IntRange { get; set; }
    }
}
