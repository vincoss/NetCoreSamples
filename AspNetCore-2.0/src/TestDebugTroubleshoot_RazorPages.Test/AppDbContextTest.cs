using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestDebugTroubleshoot_RazorPages.Data;
using Xunit;

namespace TestDebugTroubleshoot_RazorPages.Test
{
    public class AppDbContextTest
    {
        [Fact]
        public async Task GetMessagesAsync_MessagesAreReturned()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var expectedMessages = AppDbContext.GetEvents();
                await db.AddRangeAsync(expectedMessages);
                await db.SaveChangesAsync();

                // Act
                var result = await db.GetMessagesAsync();

                // Assert
                var actualMessages = Assert.IsAssignableFrom<List<EventInfo>>(result);
                Assert.Equal(
                    expectedMessages.OrderBy(m => m.Name).Select(m => m.Name),
                    actualMessages.OrderBy(m => m.Name).Select(m => m.Name));
            }
        }
    }
}
