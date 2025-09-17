using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025;
using BookStore.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class CustomerCreateParameter
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    [StringLength(20)]
    public string Phone { get; set; }

    public static Customer ToModel(CustomerCreateParameter parameter)
    {
        return new Customer
        {
            Name = parameter.Name,
            Email = parameter.Email,
            Phone = parameter.Phone ?? string.Empty
        };
    }
}