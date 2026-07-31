using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WebhookBin.App.Public.Endpoints;

public static class EndpointExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds endpoints
        /// </summary>
        /// <returns></returns>
        public IServiceCollection AddPublicApiEndpoints()
        {
            services.AddPublicApiEndpoints(Assembly.GetExecutingAssembly());
            return services;
        }

        /// <summary>
        /// Adds all IEndpoints implementations as IEnumerable of IEndpoint service.
        /// </summary>
        /// <returns></returns>
        public IServiceCollection AddPublicApiEndpoints(Assembly assembly)
        {
            var serviceDescriptors = assembly
                .DefinedTypes
                .Where(type =>
                    type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();
            
            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }
    }

    extension(WebApplication app)
    {
        /// <summary>
        /// Maps endpoint implementations of IEndpoints defined as services.
        /// </summary>
        /// <returns></returns>
        public IApplicationBuilder MapPublicApiEndpoints(RouteGroupBuilder? routeGroupBuilder = null)
        {
            var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
            IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;
            foreach (var endpoint in endpoints)
            {
             endpoint.MapEndpoint(builder);   
            }

            return app;
        }
    }
}