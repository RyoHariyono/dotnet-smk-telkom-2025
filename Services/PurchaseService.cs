using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Services;

public class PurchaseService
{
    private readonly InMemoryDbContext _inMemoryDb;

    public PurchaseService(
      InMemoryDbContext inMemoryDb
    )
    {
        _inMemoryDb = inMemoryDb;
    }

    public (IActionResult, List<PurchaseResult>) GetAll()
    {
        var results = PurchaseResult.MapModels(_inMemoryDb.Purchases);
        return (null, results);
    }

    public (IActionResult, PurchaseResult) FindOneById(Guid id)
    {
        var purchase = _inMemoryDb.Purchases.FirstOrDefault(p => p.Id == id);
        if (purchase == null)
        {
            return (new NotFoundObjectResult("Purchase not found"), null);
        }
        var result = new PurchaseResult(purchase);
        return (null, result);
    }

    public (IActionResult, PurchaseResult) Create(
      PurchaseCreateParameter parameter
    )
    {
        var purchase = PurchaseCreateParameter.ToModel(parameter);
        purchase.Id = Guid.NewGuid();
        purchase.CreatedAt = DateTime.Now;
        purchase.UpdatedAt = DateTime.Now;
        purchase.PurchasedAt = DateTime.Now;
        _inMemoryDb.Purchases.Add(purchase);

        var result = new PurchaseResult(purchase);
        return (null, result);
    }

    public (IActionResult, PurchaseResult) Delete(
      Guid id
    )
    {
        var purchase = _inMemoryDb.Purchases.FirstOrDefault(p => p.Id == id);
        if (purchase == null)
        {
            return (new NotFoundObjectResult("Purchase not found"), null);
        }
        _inMemoryDb.Purchases.Remove(purchase);
        return (null, null);
    }
}