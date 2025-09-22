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
        var results = _purchaseService.GetAll();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult FindOneById(Guid id)
    {
        var results = _purchaseService.FindOneById(id);
        return Ok(results);
    }

    [HttpPost]
    public IActionResult Create(
      [FromBody] PurchaseCreateParameter parameter
    )
    {
        var result = _purchaseService.Create(parameter);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
      Guid id
    )
    {
        _purchaseService.Delete(id);

        return Ok();
    }
}