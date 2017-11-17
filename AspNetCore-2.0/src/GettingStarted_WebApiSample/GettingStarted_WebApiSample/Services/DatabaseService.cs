using GettingStarted_WebApiSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GettingStarted_WebApiSample.Services
{
    public interface IDatabaseService
    {
        Task AddTotoAsync(TotoItem item);
        Task<TotoItem> UpdateTototAsync(TotoItem item);
        Task<TotoItem> FindTotoAsync(int id);
        Task<bool> RemoveAsync(TotoItem item);

        IEnumerable<TotoItem> TotoItems { get; }
    }

    // Just in memory database
    public class DatabaseService : IDatabaseService
    {
        private IList<TotoItem> _items;

        public DatabaseService()
        {
            _items = new List<TotoItem>();
        }

        public Task AddTotoAsync(TotoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.Id = _items.Count + 1;

            _items.Add(item);
            return Task.CompletedTask; // OR Task.FromResult(0);
        }

        public Task<TotoItem> UpdateTototAsync(TotoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var existingCustomer = _items.SingleOrDefault(x => x.Id == item.Id);

            if (existingCustomer == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            existingCustomer.Name = item.Name;
            existingCustomer.IsComplete = item.IsComplete;

            return Task.FromResult(existingCustomer);
        }

        public Task<TotoItem> FindTotoAsync(int id)
        {
            var customer = _items.SingleOrDefault(x => x.Id == id);

            return Task.FromResult(customer);
        }

        public Task<bool> RemoveAsync(TotoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = _items.Remove(item);
            return Task.FromResult(result);
        }

        public IEnumerable<TotoItem> TotoItems
        {
            get { return _items.ToArray(); }
        }
    }

    public class DbUpdateConcurrencyException : Exception
    { }

}
