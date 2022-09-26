using Microsoft.AspNetCore.Mvc;

namespace CloudCustomer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    // private readonly ILogger<UsersController> _logger;

    public UsersController()
    {
        // _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetAsync()
    {
        return Ok("All good");
    }
}