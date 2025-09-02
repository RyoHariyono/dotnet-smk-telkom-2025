using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Parameters;
using BookStore.Results;

namespace BookStore.Services
{
    public interface ICustomerService
    {
        Task<CustomerResult> CreateAsync(CustomerCreateParameter p);
        Task<CustomerResult> UpdateAsync(int id, CustomerCreateParameter p);
        Task DeleteAsync(int id);
        Task<CustomerResult> GetByIdAsync(int id);
        Task<IEnumerable<CustomerResult>> GetAllAsync();
    }
}