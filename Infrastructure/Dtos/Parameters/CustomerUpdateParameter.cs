using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025;
using BookStore.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class CustomerUpdateParameter
{
    [StringLength(100, MinimumLength = 2)]
    public string? Name { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    [StringLength(20)]
    public string? Phone { get; set; }

    public static Customer ToModel(Customer existingCustomer, CustomerUpdateParameter parameter)
    {
        existingCustomer.Name = parameter.Name ?? existingCustomer.Name;
        existingCustomer.Email = parameter.Email ?? existingCustomer.Email;
        existingCustomer.Phone = parameter.Phone ?? existingCustomer.Phone;

        return existingCustomer;
    }
}