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
    [Fact]
    public async Task GetAllUserAsync_WhenCalled_InvokesHttpGetRequest()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var mockHttpMessageHandler = MockHttpMessageHandler
            .SetupBasicGetResourceList<User>(expectedResponse);
        var mockHttpClient = new HttpClient(mockHttpMessageHandler.Object);
        var sut = new UsersService(mockHttpClient);
        
        var result = await sut.GetAllUsersAsync();

        mockHttpMessageHandler
            .Protected()
            .Verify(
                Constants.SendAsyncMethodName,
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
    }
}