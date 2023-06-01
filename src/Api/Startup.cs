using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SlottingMock.Application.Common.Interfaces;
using SlottingMock.Application.Extensions;
using SlottingMock.Infrastructure.Services.RuleEngine;

[assembly: FunctionsStartup(typeof(Api.Startup))]

namespace Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationLayer();
            builder.Services.AddTransient<IRuleEngineService, RuleEngineService>();
        }
    }
}
