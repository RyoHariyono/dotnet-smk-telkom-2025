using dotnet_smk_telkom_2025.Dtos.Parameters;
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
    public IActionResult GetAll()
    {
        var (error, results) = _customerService.GetAll();
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult FindOneById(Guid id)
    {
        var (error, results) = _customerService.FindOneById(id);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpPost]
    public IActionResult Create(
      [FromBody] CustomerCreateParameter parameter
    )
    {
        var (error, results) = _customerService.Create(parameter);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(
      Guid id,
      [FromBody] CustomerUpdateParameter parameter
    )
    {
        var (error, results) = _customerService.Update(id, parameter);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
      Guid id
    )
    {
        var (error, results) = _customerService.Delete(id);
        if (error != null)
        {
            return error;
        }

        return Ok(results);
    }
}