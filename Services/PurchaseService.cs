using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;
using Microsoft.AspNetCore.Mvc;

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

    public (IActionResult, List<PurchaseResult>) GetAll()
    {
        var purchases = _purchaseQueryRepository.FindAll();
        var results = PurchaseResult.MapModels(purchases);
        return (null, results);
    }

    public (IActionResult, PurchaseResult) FindOneById(Guid id)
    {
        try
        {
            var purchase = _purchaseQueryRepository.FindOneById(id);
            if (purchase == null)
            {
                throw new NotFoundException("Purchase not found");
            }
            var result = new PurchaseResult(purchase);
            return (null, result);
        }
        catch (NotFoundException e)
        {
            return (new NotFoundObjectResult(e.Message), null);
        }
        catch (Exception e)
        {
            return (new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError }, null);
        }
    }

    public (IActionResult, PurchaseResult) Create(
      PurchaseCreateParameter parameter
    )
    {
        var purchase = PurchaseCreateParameter.ToModel(parameter);
        purchase = _purchaseStoreRepository.Create(purchase);

        var result = new PurchaseResult(purchase);
        return (null, result);
    }

    public (IActionResult, PurchaseResult) Delete(
      Guid id
    )
    {
        try
        {
            var purchase = _purchaseQueryRepository.FindOneById(id);
            if (purchase == null)
            {
                throw new NotFoundException("Purchase not found");
            }

            _purchaseStoreRepository.DeleteById(id);
            return (null, null);
        }
        catch (NotFoundException e)
        {
            return (new NotFoundObjectResult(e.Message), null);
        }
        catch (Exception e)
        {
            return (new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError }, null);
        }
    }
}