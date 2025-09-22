using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("books")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly BookService _bookService;

    public BookController(
      ILogger<BookController> logger,
      BookService bookService
    )
    {
        _logger = logger;
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var (error, results) = _bookService.GetAll();
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult FindOneById(Guid id)
    {
        var (error, results) = _bookService.FindOneById(id);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpPost]
    public IActionResult Create(
      [FromBody] BookCreateParameter parameter
    )
    {
        var (error, results) = _bookService.Create(parameter);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(
      Guid id,
      [FromBody] BookUpdateParameter parameter
    )
    {
        var (error, results) = _bookService.Update(id, parameter);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
      Guid id
    )
    {
        var (error, results) = _bookService.Delete(id);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }
}