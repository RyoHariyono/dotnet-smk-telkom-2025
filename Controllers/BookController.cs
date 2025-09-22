using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("books")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly InMemoryDbContext _inMemoryDb;

    public BookController(
      ILogger<BookController> logger,
      InMemoryDbContext inMemoryDb
    )
    {
        _logger = logger;
        _inMemoryDb = inMemoryDb;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var results = BookResult.MapModels(_inMemoryDb.Books);
        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult FindOneById(Guid id)
    {
        var book = _inMemoryDb.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound("Book not found");
        }
        var result = new BookResult(book);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(
      [FromBody] BookCreateParameter parameter
    )
    {
        var book = BookCreateParameter.ToModel(parameter);
        book.Id = Guid.NewGuid();
        book.CreatedAt = DateTime.Now;
        book.UpdatedAt = DateTime.Now;
        _inMemoryDb.Books.Add(book);

        var result = new BookResult(book);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(
      Guid id,
      [FromBody] BookUpdateParameter parameter
    )
    {
        var book = _inMemoryDb.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound("Book not found");
        }
        book = BookUpdateParameter.ToModel(book, parameter);

        var index = _inMemoryDb.Books.FindIndex(b => b.Id == book.Id);
        if (index >= 0)
        {
            book.UpdatedAt = DateTime.Now;
            _inMemoryDb.Books[index] = book;
        }

        var result = new BookResult(book);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
      Guid id
    )
    {
        var book = _inMemoryDb.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound("Book not found");
        }
        _inMemoryDb.Books.Remove(book);
        return Ok();
    }
}