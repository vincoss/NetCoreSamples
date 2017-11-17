using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_ManagingApplicationState.Models
{
    public class RequestEntryCollection
    {
        public List<RequestEntry> Entries { get; set; } = new List<RequestEntry>();

        public void RecordRequest(string requestPath)
        {
            var existingEntry = Entries.FirstOrDefault(e => e.Path == requestPath);
            if (existingEntry != null) { existingEntry.Count++; return; }

            var newEntry = new RequestEntry()
            {
                Path = requestPath,
                Count = 1
            };
            Entries.Add(newEntry);
        }

        public int TotalCount()
        {
            return Entries.Sum(e => e.Count);
        }
    }
}