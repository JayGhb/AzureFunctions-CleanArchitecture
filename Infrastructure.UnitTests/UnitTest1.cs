using Infrastructure.Services.DynamicsCrm;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace Infrastructure.UnitTests
{
    public class HttpClientFactory_Polly_Policy_Test
    {
        const string fakeClient = "fakeClient";

        [Fact]
        public async Task Configured_policy_on_named_client_is_used_when_client_makes_request()
        {
            //Arrange 
            IServiceCollection services = new ServiceCollection();

            bool retryCalled = false;

            HttpStatusCode codeHandledByPolicy = HttpStatusCode.TooManyRequests;

            services.AddHttpClient(fakeClient)
                .AddPolicyHandler(DynamicsCrmServicePolicies.GetRetryPolicy())
                .AddHttpMessageHandler(() => new StubDelegatingHandler(codeHandledByPolicy));

            services.AddHttpClient(fakeClient)
            .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError().RetryAsync(3, onRetry: (_, __) => retryCalled = true))
            .AddHttpMessageHandler(() => new StubDelegatingHandler(codeHandledByPolicy));

            HttpClient configuredClient =
                services
                    .BuildServiceProvider()
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(fakeClient);

            //Act
            HttpResponseMessage result = await configuredClient.GetAsync("https://www.doesnotmatterwhatthisis.com/");

            //Assert
            Assert.Equal(codeHandledByPolicy, result.StatusCode);
            Assert.True(retryCalled);
        }

    }

    public class StubDelegatingHandler : DelegatingHandler
    {
        private readonly HttpStatusCode stubHttpStatusCode;
        public StubDelegatingHandler(HttpStatusCode stubHttpStatusCode) => this.stubHttpStatusCode = stubHttpStatusCode;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => Task.FromResult(new HttpResponseMessage(stubHttpStatusCode));
    }
}