using System.Runtime.InteropServices;
using CloudCustomer.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class UsersControllerTest
{
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
}