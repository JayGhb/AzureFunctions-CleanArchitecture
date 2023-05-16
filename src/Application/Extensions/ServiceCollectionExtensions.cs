using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Reservation.Application.Common.Behaviors;
using SlottingMock.Application.Common.Interfaces;
using System.Reflection;

namespace SlottingMock.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR();
            services.AddValidators();
            return services;
        }

        /// <summary>
        /// Add MediatR command and query handlers on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            return services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
