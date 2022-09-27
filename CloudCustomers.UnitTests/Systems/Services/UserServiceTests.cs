using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CloudCustomer.Api.Models;
using CloudCustomer.Api.Models.Config;
using CloudCustomer.Api.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services;

public class UserServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _mockHttpClient;
    private readonly IOptions<UsersApiOptions> _mockUsersApiOptions;
    
    private const string ENDPOINT = "https://example.com/users";
    
    public UserServiceTests()
    {
        _mockUsersApiOptions = Options.Create(new UsersApiOptions
        {
            Endpoint = ENDPOINT
        });
        
        _mockHttpMessageHandler = MockHttpMessageHandler<User>
            .SetupReturnNotFound();

        _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);
    }

    [Fact]
    public async Task GetAllUserAsync_WhenCalled_InvokesHttpGetRequest()
    {
        var sut = new UsersService(_mockHttpClient, _mockUsersApiOptions);
        var result = await sut.GetAllUsersAsync();
        _mockHttpMessageHandler
            .Protected()
            .Verify(
                Constants.SendAsyncMethod,
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()); 
    }

    [Fact]
    public async Task GetAllUsersAsync_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        var mockHttpMessageHandler = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(UsersFixture.GetTestUsers());
        var mockHttpClient = new HttpClient(mockHttpMessageHandler.Object);
        var sut = new UsersService(mockHttpClient, _mockUsersApiOptions);
        
        var result = await sut.GetAllUsersAsync();
        result.Count.Should().Be(UsersFixture.GetTestUsers().Count);
    }
    
    [Fact]
    public async Task GetAllUsersAsync_WhenStatus404NotFound_ReturnsEmptyList()
    {
        var sut = new UsersService(_mockHttpClient, _mockUsersApiOptions);
        var result = await sut.GetAllUsersAsync();
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetAllUsersAsync_WhenCalled_InvokesConfiguredExternalUrl()
    {
        var sut = new UsersService(_mockHttpClient, _mockUsersApiOptions);
        var result = await sut.GetAllUsersAsync(); 
        
        _mockHttpMessageHandler
            .Protected()
            .Verify(
                Constants.SendAsyncMethod,
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Get && req.RequestUri!.ToString() == ENDPOINT),
                ItExpr.IsAny<CancellationToken>()); 
    }
}