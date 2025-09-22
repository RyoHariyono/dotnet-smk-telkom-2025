using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("customers")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly InMemoryDbContext _inMemoryDb;

    public CustomerController(
      ILogger<CustomerController> logger,
      InMemoryDbContext inMemoryDb
    )
    {
        _logger = logger;
        _inMemoryDb = inMemoryDb;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var results = CustomerResult.MapModels(_inMemoryDb.Customers);
        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult FindOneById(Guid id)
    {
        var customer = _inMemoryDb.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }
        var result = new CustomerResult(customer);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(
      [FromBody] CustomerCreateParameter parameter
    )
    {
        var customer = CustomerCreateParameter.ToModel(parameter);
        customer.Id = Guid.NewGuid();
        customer.CreatedAt = DateTime.Now;
        customer.UpdatedAt = DateTime.Now;
        _inMemoryDb.Customers.Add(customer);

        var result = new CustomerResult(customer);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(
      Guid id,
      [FromBody] CustomerUpdateParameter parameter
    )
    {
        var customer = _inMemoryDb.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }
        customer = CustomerUpdateParameter.ToModel(customer, parameter);

        var index = _inMemoryDb.Customers.FindIndex(c => c.Id == customer.Id);
        if (index >= 0)
        {
            customer.UpdatedAt = DateTime.Now;
            _inMemoryDb.Customers[index] = customer;
        }

        var result = new CustomerResult(customer);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
      Guid id
    )
    {
        var customer = _inMemoryDb.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }
        _inMemoryDb.Customers.Remove(customer);
        return Ok();
    }
}