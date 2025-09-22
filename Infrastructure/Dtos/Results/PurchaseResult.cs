using BookStore.Models;

namespace dotnet_smk_telkom_2025.Dtos.Results;

public class PurchaseResult
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid CustomerId { get; set; }
    public string BookTitle { get; set; }
    public string CustomerName { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchasedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public PurchaseResult(
        Purchase purchase
    )
    {
        Id = purchase.Id;
        BookId = purchase.BookId;
        CustomerId = purchase.CustomerId;
        BookTitle = purchase.Book?.Title ?? "";
        CustomerName = purchase.Customer?.Name ?? "";
        Quantity = purchase.Quantity;
        TotalPrice = purchase.TotalPrice;
        PurchasedAt = purchase.PurchasedAt;
        CreatedAt = purchase.CreatedAt;
        UpdatedAt = purchase.UpdatedAt;
    }

    public static List<PurchaseResult> MapModels(
        List<Purchase> purchases
    )
    {
        return purchases.Select(purchase => new PurchaseResult(purchase)).ToList();
    }
}