using BookStore.Models;

namespace dotnet_smk_telkom_2025.Infrastructure.Databases;

public class InMemoryDbContext
{
    public List<Book> Books { get; set; } = [];
    public List<Customer> Customers { get; set; } = [];
    public List<Purchase> Purchases { get; set; } = [];
}