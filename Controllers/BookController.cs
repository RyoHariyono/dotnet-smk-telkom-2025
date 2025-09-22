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
        var results = _bookService.GetAll();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult FindOneById(Guid id)
    {
        var results = _bookService.FindOneById(id);
        return Ok(results);
    }

    [HttpPost]
    public IActionResult Create(
      [FromBody] BookCreateParameter parameter
    )
    {
        var result = _bookService.Create(parameter);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(
      Guid id,
      [FromBody] BookUpdateParameter parameter
    )
    {
        var result = _bookService.Update(id, parameter);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
      Guid id
    )
    {
        _bookService.Delete(id);

        return Ok();
    }
}