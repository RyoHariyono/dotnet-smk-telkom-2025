using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Databases;

namespace BookStore.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InMemoryDbContext _db;
        public CustomerRepository(InMemoryDbContext db) => _db = db;

        public async Task<Customer> AddAsync(Customer c)
        {
            _db.Customers.Add(c);
            await _db.SaveChangesAsync();
            return c;
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _db.Customers.FindAsync(id);
            if (c != null)
            {
                _db.Customers.Remove(c);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() => await _db.Customers.ToListAsync();

        public async Task<Customer> GetByIdAsync(int id) => await _db.Customers.FindAsync(id);

        public async Task<Customer> UpdateAsync(Customer c)
        {
            var existing = await _db.Customers.FindAsync(c.Id);
            if (existing == null) return null;
            existing.Name = c.Name;
            existing.Email = c.Email;
            existing.Phone = c.Phone;
            await _db.SaveChangesAsync();
            return existing;
        }
    }
}