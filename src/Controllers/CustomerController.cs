using Microsoft.AspNetCore.Mvc;
using SharpPayStack.Interfaces;


namespace SharpPayStack.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{

    private readonly IServiceManager _service;

    public CustomerController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        var result = await _service.CustomerService.GetCustomerAsync(id);

        return result.Success ? Ok(result) : NotFound(result);
    }
}
