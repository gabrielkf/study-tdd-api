using CloudCustomer.Api.Models;

namespace CloudCustomer.Api.Services;

public interface IUsersService
{
    public Task<List<User>> GetAllUsersAsync();
}

public class UsersService : IUsersService
{
    public async Task<List<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }
}