using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Parameters;
using BookStore.Repositories;
using BookStore.Results;

namespace BookStore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        public CustomerService(ICustomerRepository repo) => _repo = repo;

        public async Task<CustomerResult> CreateAsync(CustomerCreateParameter p)
        {
            var c = new Customer { Name = p.Name, Email = p.Email, Phone = p.Phone };
            var created = await _repo.AddAsync(c);
            return Map(created);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<IEnumerable<CustomerResult>> GetAllAsync() => (await _repo.GetAllAsync()).Select(Map);

        public async Task<CustomerResult> GetByIdAsync(int id) => Map(await _repo.GetByIdAsync(id));

        public async Task<CustomerResult> UpdateAsync(int id, CustomerCreateParameter p)
        {
            var toUpdate = new Customer { Id = id, Name = p.Name, Email = p.Email, Phone = p.Phone };
            var updated = await _repo.UpdateAsync(toUpdate);
            return Map(updated);
        }

        private static CustomerResult Map(Customer c) => c == null ? null : new CustomerResult
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone
        };
    }
}