using System.Collections.Generic;
using System.Threading.Tasks;


namespace GettingStarted_RazorPagesContacts.Services
{
    public static class Extensions
    {
        public static Task<IEnumerable<TSource>> ToListAsync<TSource>(this IEnumerable<TSource> source)
        {
            return Task.FromResult(source);
        }
    }
}
