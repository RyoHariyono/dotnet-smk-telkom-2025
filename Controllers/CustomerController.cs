using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("customers")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly CustomerService _customerService;

    public CustomerController(
      ILogger<CustomerController> logger,
      CustomerService customerService
    )
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpGet]
    public ApiResponse GetAll()
    {
        var results = _customerService.GetAll();
        return new ApiResponseList<CustomerResult>(results);
    }

    [HttpGet("{id}")]
    public ApiResponse FindOneById(Guid id)
    {
        var result = _customerService.FindOneById(id);
        return new ApiResponseData<CustomerResult>(result);
    }

    [HttpPost]
    public ApiResponse Create(
      [FromBody] CustomerCreateParameter parameter
    )
    {
        var result = _customerService.Create(parameter);
        return new ApiResponseData<CustomerResult>(result, HttpStatusCode.Created);
    }

    [HttpPatch("{id}")]
    public ApiResponse Update(
      Guid id,
      [FromBody] CustomerUpdateParameter parameter
    )
    {
        var result = _customerService.Update(id, parameter);
        return new ApiResponseData<CustomerResult>(result);
    }

    [HttpDelete("{id}")]
    public ApiResponse Delete(
      Guid id
    )
    {
        _customerService.Delete(id);

        return new ApiResponseData<CustomerResult>(null);
    }
}