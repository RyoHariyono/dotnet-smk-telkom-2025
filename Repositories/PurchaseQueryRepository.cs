using dotnet_smk_telkom_2025.Infrastructure.Databases;
using BookStore.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class PurchaseQueryRepository
{
    public InMemoryDbContext InMemoryDb { get; set; }

    public PurchaseQueryRepository(
      InMemoryDbContext inMemoryDb
    )
    {
        InMemoryDb = inMemoryDb;
    }

    public List<Purchase> FindAll()
    {
        return InMemoryDb.Purchases;
    }

    public Purchase FindOneById(Guid id)
    {
        return InMemoryDb.Purchases.FirstOrDefault(p => p.Id == id);
    }
}