using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;

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

    public List<CustomerResult> GetAll()
    {
        var customers = _customerQueryRepository.FindAll();
        var results = CustomerResult.MapModels(customers);
        return results;
    }

    public CustomerResult FindOneById(Guid id)
    {
        var customer = _customerQueryRepository.FindOneById(id);
        if (customer == null)
        {
            throw new NotFoundException("Customer not found");
        }
        var result = new CustomerResult(customer);
        return result;
    }

    public CustomerResult Create(
      CustomerCreateParameter parameter
    )
    {
        var customer = CustomerCreateParameter.ToModel(parameter);
        customer = _customerStoreRepository.Create(customer);

        var result = new CustomerResult(customer);
        return result;
    }

    public CustomerResult Update(
      Guid id,
      CustomerUpdateParameter parameter
    )
    {
        var customer = _customerQueryRepository.FindOneById(id);
        if (customer == null)
        {
            throw new NotFoundException("Customer not found");
        }
        customer = CustomerUpdateParameter.ToModel(customer, parameter);
        customer = _customerStoreRepository.UpdateById(id, customer);

        var result = new CustomerResult(customer);
        return result;
    }

    public void Delete(
      Guid id
    )
    {
        var customer = _customerQueryRepository.FindOneById(id);
        if (customer == null)
        {
            throw new NotFoundException("Customer not found");
        }

        _customerStoreRepository.DeleteById(id);
    }
}