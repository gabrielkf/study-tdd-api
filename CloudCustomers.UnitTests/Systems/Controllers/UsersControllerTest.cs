using System.Runtime.InteropServices;
using CloudCustomer.Api.Controllers;
using CloudCustomer.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class UsersControllerTest
{
    private readonly Mock<IUsersService?> _mockUsersService;
    
    public UsersControllerTest()
    {
        _mockUsersService = new Mock<IUsersService?>();
    }
    
    [Fact]
    public async void GetAsync_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var sut = new UsersController();
        // Act
        var result = (OkObjectResult)await sut.GetAsync();
        // Assert
        result.StatusCode.Should().Be(200);
    }
    
    [Fact]
    public async void GetAsync_OnSuccess_InvokesUserService()
    {
        // Arrange
        var sut = new UsersController(_mockUsersService.Object);
        // Act
        var result = (OkObjectResult)await sut.GetAsync();
        // Assert
        result.StatusCode.Should().Be(200);
    }
}