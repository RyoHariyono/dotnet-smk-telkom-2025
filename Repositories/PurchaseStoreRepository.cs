using dotnet_smk_telkom_2025.Infrastructure.Databases;
using BookStore.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class PurchaseStoreRepository
{
    public InMemoryDbContext InMemoryDb { get; set; }

    public PurchaseStoreRepository(
      InMemoryDbContext inMemoryDb
    )
    {
        InMemoryDb = inMemoryDb;
    }

    public Purchase Create(Purchase purchase)
    {
        purchase.Id = Guid.NewGuid();
        purchase.CreatedAt = DateTime.Now;
        purchase.UpdatedAt = DateTime.Now;
        purchase.PurchasedAt = DateTime.Now;
        InMemoryDb.Purchases.Add(purchase);
        return purchase;
    }

    public Purchase UpdateById(Guid id, Purchase purchase)
    {
        var index = InMemoryDb.Purchases.FindIndex(p => p.Id == id);
        if (index >= 0)
        {
            purchase.UpdatedAt = DateTime.Now;
            InMemoryDb.Purchases[index] = purchase;
        }
        return purchase;
    }

    public void DeleteById(Guid id)
    {
        var purchase = InMemoryDb.Purchases.FirstOrDefault(p => p.Id == id);
        if (purchase == null)
        {
            return;
        }
        InMemoryDb.Purchases.Remove(purchase);
    }
}