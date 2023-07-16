using Microsoft.Extensions.DependencyInjection;
using SlottingMock.Application.Common.Interfaces;
using SlottingMock.Infrastructure.Services.DynamicsCrm;
using Infrastructure.Services.DynamicsCrm;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            //services.AddDbContext();
            services.AddDynamicsCrmService();
            return services;
        }

        /*private static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TimeSlottingDb"));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            return services;
        }*/

        private static IServiceCollection AddDynamicsCrmService(this IServiceCollection services)
        {
            services.AddHttpClient<IDynamicsCrmService, DynamicsCrmService>("dynamicsCrm", client =>
            {
                client.BaseAddress = new Uri("https://yourorganizationname.api.crm.dynamics.com");
                //add more configs as needed e.g. client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(DynamicsCrmServicePolicies.GetRetryPolicy(async (_, _, _, _) => { }));
            return services;
        }
    }
}
