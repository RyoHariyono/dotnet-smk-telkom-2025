using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;

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

    public List<BookResult> GetAll()
    {
        var books = _bookQueryRepository.FindAll();
        var results = BookResult.MapModels(books);
        return results;
    }

    public BookResult FindOneById(Guid id)
    {
        var book = _bookQueryRepository.FindOneById(id);
        if (book == null)
        {
            throw new NotFoundException("Book not found");
        }
        var result = new BookResult(book);
        return result;
    }

    public BookResult Create(
      BookCreateParameter parameter
    )
    {
        var book = BookCreateParameter.ToModel(parameter);
        book = _bookStoreRepository.Create(book);

        var result = new BookResult(book);
        return result;
    }

    public BookResult Update(
      Guid id,
      BookUpdateParameter parameter
    )
    {
        var book = _bookQueryRepository.FindOneById(id);
        if (book == null)
        {
            throw new NotFoundException("Book not found");
        }
        book = BookUpdateParameter.ToModel(book, parameter);
        book = _bookStoreRepository.UpdateById(id, book);

        var result = new BookResult(book);
        return result;
    }

    public void Delete(
      Guid id
    )
    {
        var book = _bookQueryRepository.FindOneById(id);
        if (book == null)
        {
            throw new NotFoundException("Book not found");
        }

        _bookStoreRepository.DeleteById(id);
    }
}