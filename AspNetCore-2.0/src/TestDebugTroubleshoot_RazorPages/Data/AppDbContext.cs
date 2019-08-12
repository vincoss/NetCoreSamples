using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDebugTroubleshoot_RazorPages.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<EventInfo> Events { get; set; }

        public async virtual Task<List<EventInfo>> GetMessagesAsync()
        {
            return await Events
                .OrderBy(message => message.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Initialize()
        {
            Events.AddRange(GetEvents());
            SaveChanges();
        }

        public static List<EventInfo> GetEvents()
        {
            return new List<EventInfo>()
            {
                new EventInfo(){ Name = "Event one" },
                new EventInfo(){ Name = "Event two" },
                new EventInfo(){ Name = "Event three" }
            };
        }
    }
}
