using Microsoft.AspNetCore.Mvc;

namespace CloudCustomer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetAsync()
    {
        return null;
    }
}