using GettingStarted_RazorPagesContacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GettingStarted_RazorPagesContacts.Services
{
    public interface IDatabaseService
    {
        Task AddCustomerAsync(CustomerInfo customer);
        Task<CustomerInfo> UpdateCustomerAsync(CustomerInfo customer);
        Task<CustomerInfo> FindCustomerAsync(int id);
        Task<bool> RemoveAsync(CustomerInfo customer);

        IEnumerable<CustomerInfo> Customers { get; }
    }

    // Just in memory database
    public class DatabaseService : IDatabaseService
    {
        private IList<CustomerInfo> _customers;

        public DatabaseService()
        {
            _customers = new List<CustomerInfo>();
        }

        public Task AddCustomerAsync(CustomerInfo customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            _customers.Add(customer);
            return Task.CompletedTask; // OR Task.FromResult(0);
        }

        public Task<CustomerInfo> FindCustomerAsync(int id)
        {

            var customer = _customers.SingleOrDefault(x => x.Id == id);

            return Task.FromResult(customer);
        }

        public Task<bool> RemoveAsync(CustomerInfo customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var result = _customers.Remove(customer);
            return Task.FromResult(result);
        }

        public Task<CustomerInfo> UpdateCustomerAsync(CustomerInfo customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var existingCustomer = _customers.SingleOrDefault(x => x.Id == customer.Id);

            if (existingCustomer == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;

            return Task.FromResult(existingCustomer);
        }

        public IEnumerable<CustomerInfo> Customers
        {
            get { return _customers.ToArray(); }
        }
    }

    public class DbUpdateConcurrencyException : Exception
    { }

}
