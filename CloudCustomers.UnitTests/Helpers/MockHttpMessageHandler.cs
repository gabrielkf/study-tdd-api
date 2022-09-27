using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using CloudCustomer.Api.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CloudCustomers.UnitTests.Helpers;

internal static class MockHttpMessageHandler<T>
{
    internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject((expectedResponse)))
        };
        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                Constants.SendAsyncMethod,
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return handlerMock;
    }

    public static Mock<HttpMessageHandler> SetupReturnNotFound()
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent(JsonConvert.SerializeObject(String.Empty))
        };
        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                Constants.SendAsyncMethod,
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return handlerMock;
    }

    public static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse, string endpoint)
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject((expectedResponse)))
        };
        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

        var httpRequestMessage = new HttpRequestMessage()
        {
            RequestUri = new Uri(endpoint),
            Method = HttpMethod.Get
        };
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                Constants.SendAsyncMethod,
                httpRequestMessage,
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return handlerMock;
    }
}