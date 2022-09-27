using CloudCustomer.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomer.Api.Services;

public interface IUsersService
{
    public Task<List<User>> GetAllUsersAsync();
}

public class UsersService : IUsersService
{
    private readonly HttpClient _httpClient;

    public UsersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var usersResponse = await _httpClient.GetAsync("http://example.com");
        return new List<User>();
    }
}