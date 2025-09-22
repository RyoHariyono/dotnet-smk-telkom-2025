using BookStore.Models;

namespace dotnet_smk_telkom_2025.Dtos.Results;

public class BookResult
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public BookResult(
        Book book
    )
    {
        Id = book.Id;
        Title = book.Title;
        Author = book.Author;
        Description = book.Description;
        Category = book.Category;
        Price = book.Price;
        Stock = book.Stock;
        CreatedAt = book.CreatedAt;
        UpdatedAt = book.UpdatedAt;
    }

    public static List<BookResult> MapModels(
        List<Book> books
    )
    {
        return books.Select(book => new BookResult(book)).ToList();
    }
}