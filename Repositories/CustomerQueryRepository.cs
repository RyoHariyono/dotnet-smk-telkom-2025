using dotnet_smk_telkom_2025.Infrastructure.Databases;
using BookStore.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class CustomerQueryRepository
{
    public InMemoryDbContext InMemoryDb { get; set; }

    public CustomerQueryRepository(
      InMemoryDbContext inMemoryDb
    )
    {
        InMemoryDb = inMemoryDb;
    }

    public List<Customer> FindAll()
    {
        return InMemoryDb.Customers;
    }

    public Customer FindOneById(Guid id)
    {
        return InMemoryDb.Customers.FirstOrDefault(c => c.Id == id);
    }
}