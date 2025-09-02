using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Databases;

namespace BookStore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly InMemoryDbContext _db;
        public BookRepository(InMemoryDbContext db) => _db = db;

        public async Task<Book> AddAsync(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return book;
        }

        public async Task DeleteAsync(int id)
        {
            var b = await _db.Books.FindAsync(id);
            if (b != null)
            {
                _db.Books.Remove(b);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync() => await _db.Books.ToListAsync();

        public async Task<Book> GetByIdAsync(int id) => await _db.Books.FindAsync(id);

        public async Task<Book> UpdateAsync(Book book)
        {
            var existing = await _db.Books.FindAsync(book.Id);
            if (existing == null) return null;
            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Description = book.Description;
            existing.Price = book.Price;
            existing.Stock = book.Stock;
            existing.Category = book.Category;
            existing.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return existing;
        }
    }
}