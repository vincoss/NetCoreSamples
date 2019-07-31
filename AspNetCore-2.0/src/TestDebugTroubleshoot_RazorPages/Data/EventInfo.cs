using System;
using System.ComponentModel.DataAnnotations;

namespace TestDebugTroubleshoot_RazorPages.Data
{
    public class EventInfo
    {
        [Key]
        public string Name { get; set; }
    }
}
