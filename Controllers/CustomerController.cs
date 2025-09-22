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
        var results = _customerService.GetAll();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult FindOneById(Guid id)
    {
        var results = _customerService.FindOneById(id);
        return Ok(results);
    }

    [HttpPost]
    public IActionResult Create(
      [FromBody] CustomerCreateParameter parameter
    )
    {
        var result = _customerService.Create(parameter);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(
      Guid id,
      [FromBody] CustomerUpdateParameter parameter
    )
    {
        var result = _customerService.Update(id, parameter);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
      Guid id
    )
    {
        _customerService.Delete(id);

        return Ok();
    }
}