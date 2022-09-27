using System.Collections.Generic;
using CloudCustomer.Api.Models;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UsersFixture
{
    public static List<User> GetTestUsers() => new()
    {
        new User()
        {
            Name = "Bruce Wayne",
            Email = "not_batman@wayne.com",
            Address = new()
            {
                City = "Gotham",
                Street = "Batcave St",
                ZipCode = "12345"
            }
        },
        new User()
        {
            Name = "Clark Kent",
            Email = "not_superman@news.com",
            Address = new()
            {
                City = "Smallville",
                Street = "Farm St",
                ZipCode = "54321"
            }
        },
        new User()
        {
            Name = "Son Goku",
            Email = "sayajin@earth.com",
            Address = new()
            {
                City = "Namekusei",
                Street = "Nowhere St",
                ZipCode = "938475"
            }
        }
    };
}