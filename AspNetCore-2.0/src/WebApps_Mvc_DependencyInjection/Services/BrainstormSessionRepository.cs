using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_Mvc_DependencyInjection.Models;

namespace WebApps_Mvc_DependencyInjection.Services
{
    public interface IBrainstormSessionRepository
    {
        Task<BrainstormSession> GetByIdAsync(int id);
        Task<IEnumerable<BrainstormSession>> ListAsync();
        Task AddAsync(BrainstormSession session);
        Task UpdateAsync(BrainstormSession session);
    }

    public class BrainstormSessionRepository : IBrainstormSessionRepository
    {
        public Task AddAsync(BrainstormSession session)
        {
            return Task.CompletedTask;
        }

        public Task<BrainstormSession> GetByIdAsync(int id)
        {
            return Task.FromResult<BrainstormSession>(new BrainstormSession());
        }

        public Task<IEnumerable<BrainstormSession>> ListAsync()
        {
            var list = new List<BrainstormSession>();
            return Task.FromResult<IEnumerable<BrainstormSession>>(list);
        }

        public Task UpdateAsync(BrainstormSession session)
        {
            return Task.CompletedTask;
        }
    }
}
