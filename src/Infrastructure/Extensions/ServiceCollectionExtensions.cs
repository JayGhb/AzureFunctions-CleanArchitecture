using Microsoft.Extensions.DependencyInjection;
using SlottingMock.Application.Common.Interfaces;
using SlottingMock.Infrastructure.Services.ExternalApi;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            //services.AddDbContext();
            services.AddExternalApiService();
            return services;
        }

        /*private static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TimeSlottingDb"));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            return services;
        }*/

        private static IServiceCollection AddExternalApiService(this IServiceCollection services)
        {
            services.AddTransient<IExternalApiService, ExternalApiService>();
            return services;
        }
    }
}
