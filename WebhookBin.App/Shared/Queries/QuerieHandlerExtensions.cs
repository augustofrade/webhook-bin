using System.Reflection;

namespace WebhookBin.App.Shared.Queries;

public static class QueryHandlerExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds all Query Handlers for the vertical slices features.
        /// </summary>
        /// <returns></returns>
        public IServiceCollection AddQueryHandlers()
        {
            var handlerTypes = Assembly.GetExecutingAssembly()
                .DefinedTypes
                .Where(t => t is { IsAbstract: false, IsInterface: false } &&
                            t.ImplementedInterfaces.Any(i =>
                                i.IsGenericType &&
                                (i.GetGenericTypeDefinition() == typeof(IQueryHandler<>) ||
                                 i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))));

            foreach (var handlerType in handlerTypes)
                services.AddScoped(handlerType);

            return services;
        }
    }
}