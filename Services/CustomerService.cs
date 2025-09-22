using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Services;

public class CustomerService
{
    private readonly CustomerQueryRepository _customerQueryRepository;
    private readonly CustomerStoreRepository _customerStoreRepository;

    public CustomerService(
      CustomerQueryRepository customerQueryRepository,
      CustomerStoreRepository customerStoreRepository
    )
    {
        _customerQueryRepository = customerQueryRepository;
        _customerStoreRepository = customerStoreRepository;
    }

    public (IActionResult, List<CustomerResult>) GetAll()
    {
        var customers = _customerQueryRepository.FindAll();
        var results = CustomerResult.MapModels(customers);
        return (null, results);
    }

    public (IActionResult, CustomerResult) FindOneById(Guid id)
    {
        try
        {
            var customer = _customerQueryRepository.FindOneById(id);
            if (customer == null)
            {
                throw new NotFoundException("Customer not found");
            }
            var result = new CustomerResult(customer);
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

    public (IActionResult, CustomerResult) Create(
      CustomerCreateParameter parameter
    )
    {
        var customer = CustomerCreateParameter.ToModel(parameter);
        customer = _customerStoreRepository.Create(customer);

        var result = new CustomerResult(customer);
        return (null, result);
    }

    public (IActionResult, CustomerResult) Update(
      Guid id,
      CustomerUpdateParameter parameter
    )
    {
        try
        {
            var customer = _customerQueryRepository.FindOneById(id);
            if (customer == null)
            {
                throw new NotFoundException("Customer not found");
            }
            customer = CustomerUpdateParameter.ToModel(customer, parameter);
            customer = _customerStoreRepository.UpdateById(id, customer);

            var result = new CustomerResult(customer);
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

    public (IActionResult, CustomerResult) Delete(
      Guid id
    )
    {
        try
        {
            var customer = _customerQueryRepository.FindOneById(id);
            if (customer == null)
            {
                throw new NotFoundException("Customer not found");
            }

            _customerStoreRepository.DeleteById(id);
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