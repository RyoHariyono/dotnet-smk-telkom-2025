using dotnet_smk_telkom_2025.Dtos.Parameters;
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
    public IActionResult GetAll()
    {
        var (error, results) = _purchaseService.GetAll();
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult FindOneById(Guid id)
    {
        var (error, results) = _purchaseService.FindOneById(id);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpPost]
    public IActionResult Create(
      [FromBody] PurchaseCreateParameter parameter
    )
    {
        var (error, results) = _purchaseService.Create(parameter);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
      Guid id
    )
    {
        var (error, results) = _purchaseService.Delete(id);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }
}