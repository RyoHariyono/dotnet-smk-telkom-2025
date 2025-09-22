using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("purchases")]
public class PurchaseController : ControllerBase
{
    private readonly ILogger<PurchaseController> _logger;
    private readonly PurchaseService _purchaseService;

    public PurchaseController(
      ILogger<PurchaseController> logger,
      PurchaseService purchaseService
    )
    {
        _logger = logger;
        _purchaseService = purchaseService;
    }

    [HttpGet]
    public ApiResponse GetAll()
    {
        var results = _purchaseService.GetAll();
        return new ApiResponseList<PurchaseResult>(results);
    }

    [HttpGet("{id}")]
    public ApiResponse FindOneById(Guid id)
    {
        var result = _purchaseService.FindOneById(id);
        return new ApiResponseData<PurchaseResult>(result);
    }

    [HttpPost]
    public ApiResponse Create(
      [FromBody] PurchaseCreateParameter parameter
    )
    {
        var result = _purchaseService.Create(parameter);
        return new ApiResponseData<PurchaseResult>(result, HttpStatusCode.Created);
    }

    [HttpDelete("{id}")]
    public ApiResponse Delete(
      Guid id
    )
    {
        _purchaseService.Delete(id);

        return new ApiResponseData<PurchaseResult>(null);
    }
}