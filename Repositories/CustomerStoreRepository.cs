using dotnet_smk_telkom_2025.Infrastructure.Databases;
using BookStore.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class CustomerStoreRepository
{
    public InMemoryDbContext InMemoryDb { get; set; }

    public CustomerStoreRepository(
      InMemoryDbContext inMemoryDb
    )
    {
        InMemoryDb = inMemoryDb;
    }

    public Customer Create(Customer customer)
    {
        customer.Id = Guid.NewGuid();
        customer.CreatedAt = DateTime.Now;
        customer.UpdatedAt = DateTime.Now;
        InMemoryDb.Customers.Add(customer);
        return customer;
    }

    public Customer UpdateById(Guid id, Customer customer)
    {
        var index = InMemoryDb.Customers.FindIndex(c => c.Id == id);
        if (index >= 0)
        {
            customer.UpdatedAt = DateTime.Now;
            InMemoryDb.Customers[index] = customer;
        }
        return customer;
    }

    public void DeleteById(Guid id)
    {
        var customer = InMemoryDb.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return;
        }
        InMemoryDb.Customers.Remove(customer);
    }
}