using BookStore.Models;

namespace dotnet_smk_telkom_2025.Dtos.Results;

public class CustomerResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public CustomerResult(
        Customer customer
    )
    {
        Id = customer.Id;
        Name = customer.Name;
        Email = customer.Email;
        Phone = customer.Phone;
        CreatedAt = customer.CreatedAt;
        UpdatedAt = customer.UpdatedAt;
    }

    public static List<CustomerResult> MapModels(
        List<Customer> customers
    )
    {
        return customers.Select(customer => new CustomerResult(customer)).ToList();
    }
}