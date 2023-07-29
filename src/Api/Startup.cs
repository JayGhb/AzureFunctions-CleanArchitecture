using Infrastructure.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Application.Extensions;

[assembly: FunctionsStartup(typeof(Api.Startup))]

namespace Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructureLayer();
        }
    }
}
