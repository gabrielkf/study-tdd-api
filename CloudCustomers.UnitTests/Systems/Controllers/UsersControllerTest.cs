using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CloudCustomer.Api.Controllers;
using CloudCustomer.Api.Models;
using CloudCustomer.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class UsersControllerTest
{
    private readonly Mock<IUsersService> _mockUsersService;
    
    public UsersControllerTest()
    {
        _mockUsersService = new Mock<IUsersService>();
        _mockUsersService
            .Setup(service => service.GetAllUsersAsync())
            .ReturnsAsync(new List<User>()
            {
                new User() 
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john@doe.com",
                    Address = new Address()
                    {
                        City = "London",
                        Street = "Baker Street",
                        ZipCode = "12345"
                    }
                }
            });
    }
    
    [Fact]
    public async void GetAsync_OnSuccess_ReturnsStatusCode200()
    {
        var sut = new UsersController(_mockUsersService.Object);
        var result = (OkObjectResult)await sut.GetAsync();
        result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async Task GetAsync_OnSuccess_InvokesUserServiceOnce()
    {
        var sut = new UsersController(_mockUsersService.Object);
        var result = await sut.GetAsync();
        _mockUsersService.Verify(
            service => service.GetAllUsersAsync(),
            Times.Once());
    }

    [Fact]
    public async Task GetAsync_OnSuccess_ReturnsListOfUsers()
    {
        var sut = new UsersController(_mockUsersService.Object);
        var result = await sut.GetAsync();
        var objectResult = (OkObjectResult)result;
        
        result.Should().BeOfType<OkObjectResult>();
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task GetAsync_OnNoUsersFound_Returns404()
    {
        _mockUsersService
            .Setup(service => service.GetAllUsersAsync())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(_mockUsersService.Object);
        var result = await sut.GetAsync();
        var objectResult = (NotFoundObjectResult)result;
        
        result.Should().BeOfType<NotFoundObjectResult>();
        objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}