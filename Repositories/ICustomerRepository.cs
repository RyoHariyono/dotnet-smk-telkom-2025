using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer c);
        Task<Customer> UpdateAsync(Customer c);
        Task DeleteAsync(int id);
        Task<Customer> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}