using Infrastructure.Services.YourExternal;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Infrastructure.UnitTests.Services.YourExternal
{
    /// <summary>
    /// These unit tests are inspired by https://github.com/App-vNext/Polly/issues/555#issuecomment-451594435. <br/>
    /// The <see cref="YourExternalServicePolicies.GetRetryPolicy(Func{Polly.DelegateResult{HttpResponseMessage}, TimeSpan, int, Polly.Context, Task})"/> <br/> 
    /// method gets a function that simply marks a boolean value as true, as its onRetryAsync method, indicating that the retries have been executed.
    /// </summary>
    /// <returns></returns>
    public class HttpClientFactory_Polly_Policy_Test
    {
        const string fakeClient = "fakeClient";

        [Fact]
        public async Task Retry_policy_on_named_client_is_used_when_client_makes_request()
        {
            //Arrange 
            IServiceCollection services = new ServiceCollection();

            bool retryCalled = false;

            HttpStatusCode statusCodeHandledByPolicy = HttpStatusCode.TooManyRequests;

            services.AddHttpClient(fakeClient)
            .AddPolicyHandler(YourExternalServicePolicies.GetRetryPolicy(async (_, _, _, _) => retryCalled = true))
            .AddHttpMessageHandler(() => new FakeDelegatingHandler(statusCodeHandledByPolicy, "Retry-After", "1"));
            
            HttpClient configuredClient =
                services
                    .BuildServiceProvider()
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(fakeClient);

            //Act
            HttpResponseMessage result = await configuredClient.GetAsync("https://www.doesnotmatterwhatthisis.com/");

            //Assert
            Assert.Equal(statusCodeHandledByPolicy, result.StatusCode);
            Assert.True(retryCalled);
        }

        [Fact]
        public async Task Retry_policy_is_not_used_if_RetryAfter_header_does_not_exist_in_ResponseMessage()
        {
            //Arrange 
            IServiceCollection services = new ServiceCollection();

            bool retryCalled = false;

            HttpStatusCode statusCodeHandledByPolicy = HttpStatusCode.TooManyRequests;

            services.AddHttpClient(fakeClient)
            .AddPolicyHandler(YourExternalServicePolicies.GetRetryPolicy(async (_, _, _, _) => retryCalled = true))
            .AddHttpMessageHandler(() => new FakeDelegatingHandler(statusCodeHandledByPolicy, string.Empty, string.Empty));

            HttpClient configuredClient =
                services
                    .BuildServiceProvider()
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(fakeClient);

            //Act
            HttpResponseMessage result = await configuredClient.GetAsync("https://www.doesnotmatterwhatthisis.com/");

            //Assert
            Assert.Equal(statusCodeHandledByPolicy, result.StatusCode);
            Assert.False(retryCalled);
        }

        [Fact]
        public async Task Retry_policy_is_not_used_if_returned_statusCode_is_not_TooManyRequests()
        {
            //Arrange 
            IServiceCollection services = new ServiceCollection();

            bool retryCalled = false;

            HttpStatusCode statusCodeHandledByPolicy = HttpStatusCode.NotFound;

            services.AddHttpClient(fakeClient)
            .AddPolicyHandler(YourExternalServicePolicies.GetRetryPolicy(async (_, _, _, _) => retryCalled = true))
            .AddHttpMessageHandler(() => new FakeDelegatingHandler(statusCodeHandledByPolicy, "Retry-After", "1"));

            HttpClient configuredClient =
                services
                    .BuildServiceProvider()
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(fakeClient);

            //Act
            HttpResponseMessage result = await configuredClient.GetAsync("https://www.doesnotmatterwhatthisis.com/");

            //Assert
            Assert.Equal(statusCodeHandledByPolicy, result.StatusCode);
            Assert.False(retryCalled);
        }
    }

    internal class FakeDelegatingHandler : DelegatingHandler
    {
        private readonly HttpStatusCode _stubHttpStatusCode;
        private readonly HttpResponseMessage _responseMessage;
        internal FakeDelegatingHandler(HttpStatusCode stubHttpStatusCode, string headerName, string headerValue)
        {
            _stubHttpStatusCode = stubHttpStatusCode;
            _responseMessage = new HttpResponseMessage(_stubHttpStatusCode);
            if(!string.IsNullOrEmpty(headerName) && !string.IsNullOrEmpty(headerValue))
                _responseMessage.Headers.Add(headerName, headerValue);
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => Task.FromResult(_responseMessage);
    }
}