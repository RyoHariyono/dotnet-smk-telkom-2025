using dotnet_smk_telkom_2025.Infrastructure.Databases;
using BookStore.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class BookStoreRepository
{
    public InMemoryDbContext InMemoryDb { get; set; }

    public BookStoreRepository(
      InMemoryDbContext inMemoryDb
    )
    {
        InMemoryDb = inMemoryDb;
    }

    public Book Create(Book book)
    {
        book.Id = Guid.NewGuid();
        book.CreatedAt = DateTime.Now;
        book.UpdatedAt = DateTime.Now;
        InMemoryDb.Books.Add(book);
        return book;
    }

    public Book UpdateById(Guid id, Book book)
    {
        var index = InMemoryDb.Books.FindIndex(b => b.Id == id);
        if (index >= 0)
        {
            book.UpdatedAt = DateTime.Now;
            InMemoryDb.Books[index] = book;
        }
        return book;
    }

    public void DeleteById(Guid id)
    {
        var book = InMemoryDb.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return;
        }
        InMemoryDb.Books.Remove(book);
    }
}