using System.Net;
using System.Text.Json.Serialization;
using CloudCustomer.Api.Models;
using CloudCustomer.Api.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CloudCustomer.Api.Services;

public interface IUsersService
{
    public Task<List<User>> GetAllUsersAsync();
}

public class UsersService : IUsersService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<UsersApiOptions> _apiConfig;

    public UsersService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var usersResponse = await _httpClient.GetAsync(_apiConfig.Value.Endpoint);
        if (usersResponse.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<User>();
        }
        var responseContent = usersResponse.Content;
        var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
        return allUsers.ToList();
    }
}