using Infrastructure.Services.DynamicsCrm;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Infrastructure.UnitTests.Services.DynamicsCrm
{
    public class HttpClientFactory_Polly_Policy_Test
    {
        const string fakeClient = "fakeClient";

        /// <summary>
        /// This unit test is inspired by https://github.com/App-vNext/Polly/issues/555#issuecomment-451594435. <br/>
        /// The <see cref="DynamicsCrmServicePolicies.GetRetryPolicy(Func{Polly.DelegateResult{HttpResponseMessage}, TimeSpan, int, Polly.Context, Task})"/> <br/> 
        /// method gets a function that simply marks a boolean value as true, as its onRetryAsync method, indicating that the retries have been executed.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Configured_policy_on_named_client_is_used_when_client_makes_request()
        {
            //Arrange 
            IServiceCollection services = new ServiceCollection();

            bool retryCalled = false;

            HttpStatusCode codeHandledByPolicy = HttpStatusCode.TooManyRequests;

            services.AddHttpClient(fakeClient)
            .AddPolicyHandler(DynamicsCrmServicePolicies.GetRetryPolicy(async (_, _, _, _) => retryCalled = true))
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
        private readonly HttpStatusCode _stubHttpStatusCode;
        private readonly HttpResponseMessage _responseMessage;
        public StubDelegatingHandler(HttpStatusCode stubHttpStatusCode)
        {
            _stubHttpStatusCode = stubHttpStatusCode;
            _responseMessage = new HttpResponseMessage(_stubHttpStatusCode);
            _responseMessage.Headers.Add("Retry-After", "1");
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => Task.FromResult(_responseMessage);
    }
}