using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("purchases")]
public class PurchaseController : ControllerBase
{
    private readonly ILogger<PurchaseController> _logger;
    private readonly InMemoryDbContext _inMemoryDb;

    public PurchaseController(
      ILogger<PurchaseController> logger,
      InMemoryDbContext inMemoryDb
    )
    {
        _logger = logger;
        _inMemoryDb = inMemoryDb;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var results = PurchaseResult.MapModels(_inMemoryDb.Purchases);
        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult FindOneById(Guid id)
    {
        var purchase = _inMemoryDb.Purchases.FirstOrDefault(p => p.Id == id);
        if (purchase == null)
        {
            return NotFound("Purchase not found");
        }
        var result = new PurchaseResult(purchase);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(
      [FromBody] PurchaseCreateParameter parameter
    )
    {
        var purchase = PurchaseCreateParameter.ToModel(parameter);
        purchase.Id = Guid.NewGuid();
        purchase.CreatedAt = DateTime.Now;
        purchase.UpdatedAt = DateTime.Now;
        purchase.PurchasedAt = DateTime.Now;
        _inMemoryDb.Purchases.Add(purchase);

        var result = new PurchaseResult(purchase);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(
      Guid id,
      [FromBody] PurchaseUpdateParameter parameter
    )
    {
        var purchase = _inMemoryDb.Purchases.FirstOrDefault(p => p.Id == id);
        if (purchase == null)
        {
            return NotFound("Purchase not found");
        }
        purchase = PurchaseUpdateParameter.ToModel(purchase, parameter);

        var index = _inMemoryDb.Purchases.FindIndex(p => p.Id == purchase.Id);
        if (index >= 0)
        {
            purchase.UpdatedAt = DateTime.Now;
            _inMemoryDb.Purchases[index] = purchase;
        }

        var result = new PurchaseResult(purchase);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
      Guid id
    )
    {
        var purchase = _inMemoryDb.Purchases.FirstOrDefault(p => p.Id == id);
        if (purchase == null)
        {
            return NotFound("Purchase not found");
        }
        _inMemoryDb.Purchases.Remove(purchase);
        return Ok();
    }
}