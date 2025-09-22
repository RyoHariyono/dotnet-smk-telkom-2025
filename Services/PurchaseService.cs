using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;

namespace dotnet_smk_telkom_2025.Services;

public class PurchaseService
{
    private readonly PurchaseQueryRepository _purchaseQueryRepository;
    private readonly PurchaseStoreRepository _purchaseStoreRepository;

    public PurchaseService(
      PurchaseQueryRepository purchaseQueryRepository,
      PurchaseStoreRepository purchaseStoreRepository
    )
    {
        _purchaseQueryRepository = purchaseQueryRepository;
        _purchaseStoreRepository = purchaseStoreRepository;
    }

    public List<PurchaseResult> GetAll()
    {
        var purchases = _purchaseQueryRepository.FindAll();
        var results = PurchaseResult.MapModels(purchases);
        return results;
    }

    public PurchaseResult FindOneById(Guid id)
    {
        var purchase = _purchaseQueryRepository.FindOneById(id);
        if (purchase == null)
        {
            throw new NotFoundException("Purchase not found");
        }
        var result = new PurchaseResult(purchase);
        return result;
    }

    public PurchaseResult Create(
      PurchaseCreateParameter parameter
    )
    {
        var purchase = PurchaseCreateParameter.ToModel(parameter);
        purchase = _purchaseStoreRepository.Create(purchase);

        var result = new PurchaseResult(purchase);
        return result;
    }

    public void Delete(
      Guid id
    )
    {
        var purchase = _purchaseQueryRepository.FindOneById(id);
        if (purchase == null)
        {
            throw new NotFoundException("Purchase not found");
        }

        _purchaseStoreRepository.DeleteById(id);
    }
}