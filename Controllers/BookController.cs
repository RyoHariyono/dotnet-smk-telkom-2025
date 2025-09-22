using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
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
    public ApiResponse GetAll()
    {
        var results = _bookService.GetAll();
        return new ApiResponseList<BookResult>(results);
    }

    [HttpGet("{id}")]
    public ApiResponse FindOneById(Guid id)
    {
        var result = _bookService.FindOneById(id);
        return new ApiResponseData<BookResult>(result);
    }

    [HttpPost]
    public ApiResponse Create(
      [FromBody] BookCreateParameter parameter
    )
    {
        var result = _bookService.Create(parameter);
        return new ApiResponseData<BookResult>(result, HttpStatusCode.Created);
    }

    [HttpPatch("{id}")]
    public ApiResponse Update(
      Guid id,
      [FromBody] BookUpdateParameter parameter
    )
    {
        var result = _bookService.Update(id, parameter);
        return new ApiResponseData<BookResult>(result);
    }

    [HttpDelete("{id}")]
    public ApiResponse Delete(
      Guid id
    )
    {
        _bookService.Delete(id);

        return new ApiResponseData<BookResult>(null);
    }
}