using System.ComponentModel.DataAnnotations;
using BookStore.Models;
using dotnet_smk_telkom_2025;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class BookCreateParameter
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Author { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Category { get; set; }

    [Required]
    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, 110000)]
    public int Stock { get; set; }

    public static Book ToModel(BookCreateParameter parameter)
    {
        return new Book
        {
            Title = parameter.Title,
            Author = parameter.Author,
            Description = parameter.Description ?? string.Empty,
            Category = parameter.Category,
            Price = parameter.Price,
            Stock = parameter.Stock
        };
    }
}