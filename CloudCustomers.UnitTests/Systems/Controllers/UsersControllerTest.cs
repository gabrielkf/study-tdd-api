using System.Collections.Generic;
using CloudCustomer.Api.Controllers;
using CloudCustomer.Api.Models;
using CloudCustomer.Api.Services;
using FluentAssertions;
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
            .Setup(service => service!.GetAllUsersAsync())
            .ReturnsAsync(new List<User>());
    }
    
    [Fact]
    public async void GetAsync_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var sut = new UsersController(_mockUsersService.Object);
        // Act
        var result = (OkObjectResult)await sut.GetAsync();
        // Assert
        result.StatusCode.Should().Be(200);
    }
    
    [Fact]
    public async void GetAsync_OnSuccess_InvokesUserServiceOnce()
    {
        // Arrange
        var sut = new UsersController(_mockUsersService.Object);
        // Act
        var result = await sut.GetAsync();
        // Assert
        _mockUsersService.Verify(
            service => service.GetAllUsersAsync(),
            Times.Once());
    }
}