using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025;
using BookStore.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class BookUpdateParameter
{
    [StringLength(200, MinimumLength = 3)]
    public string? Title { get; set; }

    [StringLength(100, MinimumLength = 2)]
    public string? Author { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(50, MinimumLength = 2)]
    public string? Category { get; set; }

    [Range(0.01, 1000000)]
    public decimal? Price { get; set; }

    [Range(0, 110000)]
    public int? Stock { get; set; }

    public static Book ToModel(Book existingBook, BookUpdateParameter parameter)
    {
        existingBook.Title = parameter.Title ?? existingBook.Title;
        existingBook.Author = parameter.Author ?? existingBook.Author;
        existingBook.Description = parameter.Description ?? existingBook.Description;
        existingBook.Category = parameter.Category ?? existingBook.Category;
        existingBook.Price = parameter.Price ?? existingBook.Price;
        existingBook.Stock = parameter.Stock ?? existingBook.Stock;

        return existingBook;
    }

}