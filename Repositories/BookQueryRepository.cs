using BookStore.Models;
using dotnet_smk_telkom_2025.Infrastructure.Databases;

namespace dotnet_smk_telkom_2025.Repositories;

public class BookQueryRepository
{
    public InMemoryDbContext InMemoryDb { get; set; }

    public BookQueryRepository(
      InMemoryDbContext inMemoryDb
    )
    {
        InMemoryDb = inMemoryDb;
    }

    public List<Book> FindAll()
    {
        return InMemoryDb.Books;
    }

    public Book FindOneById(Guid id)
    {
        return InMemoryDb.Books.FirstOrDefault(b => b.Id == id);
    }
}