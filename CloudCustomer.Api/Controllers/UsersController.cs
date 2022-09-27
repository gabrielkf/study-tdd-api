using CloudCustomer.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetAsync()
    {
        var users = await _usersService.GetAllUsersAsync();
        return Ok(users);
    }
}