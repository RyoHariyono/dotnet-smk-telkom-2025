using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Parameters;
using BookStore.Repositories;
using BookStore.Results;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        public BookService(IBookRepository repo) => _repo = repo;

        public async Task<BookResult> CreateAsync(BookCreateParameter p)
        {
            var book = new Book
            {
                Title = p.Title,
                Author = p.Author,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                Stock = p.Stock
            };
            var created = await _repo.AddAsync(book);
            return Map(created);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<IEnumerable<BookResult>> GetAllAsync() => (await _repo.GetAllAsync()).Select(Map);

        public async Task<BookResult> GetByIdAsync(int id) => Map(await _repo.GetByIdAsync(id));

        public async Task<BookResult> UpdateAsync(int id, BookCreateParameter p)
        {
            var toUpdate = new Book
            {
                Id = id,
                Title = p.Title,
                Author = p.Author,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                Stock = p.Stock
            };
            var updated = await _repo.UpdateAsync(toUpdate);
            return Map(updated);
        }

        private static BookResult Map(Book b) => b == null ? null : new BookResult
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            Description = b.Description,
            Category = b.Category,
            Price = b.Price,
            Stock = b.Stock,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt
        };
    }
}