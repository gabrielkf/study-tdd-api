using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CloudCustomer.Api.Models;
using CloudCustomer.Api.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services;

public class UserServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _mockHttpClient;
    
    public UserServiceTests()
    {
        _mockHttpMessageHandler = MockHttpMessageHandler
            .SetupBasicGetResourceList<User>(UsersFixture.GetTestUsers());

        _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);
    }

    [Fact]
    public async Task GetAllUserAsync_WhenCalled_InvokesHttpGetRequest()
    {
        var sut = new UsersService(_mockHttpClient);
        var result = await sut.GetAllUsersAsync();
        _mockHttpMessageHandler
            .Protected()
            .Verify(
                Constants.SendAsyncMethodName,
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetAllUsersAsync_ReturnsListOfUsers()
    {
        var sut = new UsersService(_mockHttpClient);
        var result = await sut.GetAllUsersAsync();
        result.Should().BeOfType<List<User>>();
    }
}