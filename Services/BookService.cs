using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Services;

public class BookService
{
    private readonly BookQueryRepository _bookQueryRepository;
    private readonly BookStoreRepository _bookStoreRepository;

    public BookService(
      BookQueryRepository bookQueryRepository,
      BookStoreRepository bookStoreRepository
    )
    {
        _bookQueryRepository = bookQueryRepository;
        _bookStoreRepository = bookStoreRepository;
    }

    public (IActionResult, List<BookResult>) GetAll()
    {
        var books = _bookQueryRepository.FindAll();
        var results = BookResult.MapModels(books);
        return (null, results);
    }

    public (IActionResult, BookResult) FindOneById(Guid id)
    {
        try
        {
            var book = _bookQueryRepository.FindOneById(id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            var result = new BookResult(book);
            return (null, result);
        }
        catch (NotFoundException e)
        {
            return (new NotFoundObjectResult(e.Message), null);
        }
        catch (Exception e)
        {
            return (new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError }, null);
        }
    }

    public (IActionResult, BookResult) Create(
      BookCreateParameter parameter
    )
    {
        var book = BookCreateParameter.ToModel(parameter);
        book = _bookStoreRepository.Create(book);

        var result = new BookResult(book);
        return (null, result);
    }

    public (IActionResult, BookResult) Update(
      Guid id,
      BookUpdateParameter parameter
    )
    {
        try
        {
            var book = _bookQueryRepository.FindOneById(id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            book = BookUpdateParameter.ToModel(book, parameter);
            book = _bookStoreRepository.UpdateById(id, book);

            var result = new BookResult(book);
            return (null, result);
        }
        catch (NotFoundException e)
        {
            return (new NotFoundObjectResult(e.Message), null);
        }
        catch (Exception e)
        {
            return (new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError }, null);
        }
    }

    public (IActionResult, BookResult) Delete(
      Guid id
    )
    {
        try
        {
            var book = _bookQueryRepository.FindOneById(id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            _bookStoreRepository.DeleteById(id);
            return (null, null);
        }
        catch (NotFoundException e)
        {
            return (new NotFoundObjectResult(e.Message), null);
        }
        catch (Exception e)
        {
            return (new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError }, null);
        }
    }
}