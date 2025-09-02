using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Parameters;
using BookStore.Results;

namespace BookStore.Services
{
    public interface IBookService
    {
        Task<BookResult> CreateAsync(BookCreateParameter param);
        Task<BookResult> UpdateAsync(int id, BookCreateParameter param);
        Task DeleteAsync(int id);
        Task<BookResult> GetByIdAsync(int id);
        Task<IEnumerable<BookResult>> GetAllAsync();
    }
}