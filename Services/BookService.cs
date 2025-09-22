using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Services;

public class BookService
{
    private readonly InMemoryDbContext _inMemoryDb;

    public BookService(
      InMemoryDbContext inMemoryDb
    )
    {
        _inMemoryDb = inMemoryDb;
    }

    public (IActionResult, List<BookResult>) GetAll()
    {
        var results = BookResult.MapModels(_inMemoryDb.Books);
        return (null, results);
    }

    public (IActionResult, BookResult) FindOneById(Guid id)
    {
        var book = _inMemoryDb.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return (new NotFoundObjectResult("Book not found"), null);
        }
        var result = new BookResult(book);
        return (null, result);
    }

    public (IActionResult, BookResult) Create(
      BookCreateParameter parameter
    )
    {
        var book = BookCreateParameter.ToModel(parameter);
        book.Id = Guid.NewGuid();
        book.CreatedAt = DateTime.Now;
        book.UpdatedAt = DateTime.Now;
        _inMemoryDb.Books.Add(book);

        var result = new BookResult(book);
        return (null, result);
    }

    public (IActionResult, BookResult) Update(
      Guid id,
      BookUpdateParameter parameter
    )
    {
        var book = _inMemoryDb.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return (new NotFoundObjectResult("Book not found"), null);
        }
        book = BookUpdateParameter.ToModel(book, parameter);

        var index = _inMemoryDb.Books.FindIndex(b => b.Id == book.Id);
        if (index >= 0)
        {
            book.UpdatedAt = DateTime.Now;
            _inMemoryDb.Books[index] = book;
        }

        var result = new BookResult(book);
        return (null, result);
    }

    public (IActionResult, BookResult) Delete(
      Guid id
    )
    {
        var book = _inMemoryDb.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return (new NotFoundObjectResult("Book not found"), null);
        }
        _inMemoryDb.Books.Remove(book);
        return (null, null);
    }
}