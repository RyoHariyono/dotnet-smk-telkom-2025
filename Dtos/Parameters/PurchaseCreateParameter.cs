using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025;
using BookStore.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class PurchaseCreateParameter
{
    [Required]
    public Guid BookId { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    [Range(1, 100)]
    public int Quantity { get; set; }


    // [Required]
    // [Range(0.01, 1000000)]
    // public decimal TotalPrice { get; set; }

    public static Purchase ToModel(PurchaseCreateParameter parameter)
    {
        return new Purchase
        {
            BookId = parameter.BookId,
            CustomerId = parameter.CustomerId,
            Quantity = parameter.Quantity,
            // TotalPrice = parameter.TotalPrice,
            PurchasedAt = DateTime.UtcNow,
        };
    }
}