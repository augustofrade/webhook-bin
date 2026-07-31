using System.Reflection;

namespace WebhookBin.App.Shared.Commands;

public static class CommandHandlerExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds all Command Handlers for the vertical slices features.
        /// </summary>
        /// <returns></returns>
        public IServiceCollection AddCommandHandlers()
        {
            var handlerTypes = Assembly.GetExecutingAssembly()
                .DefinedTypes
                .Where(t => t is { IsAbstract: false, IsInterface: false } &&
                            t.ImplementedInterfaces.Any(i =>
                                i.IsGenericType &&
                                (i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                                 i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))));

            foreach (var handlerType in handlerTypes)
                services.AddScoped(handlerType);

            return services;
        }
    }
}