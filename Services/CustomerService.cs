using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Services;

public class CustomerService
{
    private readonly InMemoryDbContext _inMemoryDb;

    public CustomerService(
      InMemoryDbContext inMemoryDb
    )
    {
        _inMemoryDb = inMemoryDb;
    }

    public (IActionResult, List<CustomerResult>) GetAll()
    {
        var results = CustomerResult.MapModels(_inMemoryDb.Customers);
        return (null, results);
    }

    public (IActionResult, CustomerResult) FindOneById(Guid id)
    {
        var customer = _inMemoryDb.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return (new NotFoundObjectResult("Customer not found"), null);
        }
        var result = new CustomerResult(customer);
        return (null, result);
    }

    public (IActionResult, CustomerResult) Create(
      CustomerCreateParameter parameter
    )
    {
        var customer = CustomerCreateParameter.ToModel(parameter);
        customer.Id = Guid.NewGuid();
        customer.CreatedAt = DateTime.Now;
        customer.UpdatedAt = DateTime.Now;
        _inMemoryDb.Customers.Add(customer);

        var result = new CustomerResult(customer);
        return (null, result);
    }

    public (IActionResult, CustomerResult) Update(
      Guid id,
      CustomerUpdateParameter parameter
    )
    {
        var customer = _inMemoryDb.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return (new NotFoundObjectResult("Customer not found"), null);
        }
        customer = CustomerUpdateParameter.ToModel(customer, parameter);

        var index = _inMemoryDb.Customers.FindIndex(c => c.Id == customer.Id);
        if (index >= 0)
        {
            customer.UpdatedAt = DateTime.Now;
            _inMemoryDb.Customers[index] = customer;
        }

        var result = new CustomerResult(customer);
        return (null, result);
    }

    public (IActionResult, CustomerResult) Delete(
      Guid id
    )
    {
        var customer = _inMemoryDb.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return (new NotFoundObjectResult("Customer not found"), null);
        }
        _inMemoryDb.Customers.Remove(customer);
        return (null, null);
    }
}