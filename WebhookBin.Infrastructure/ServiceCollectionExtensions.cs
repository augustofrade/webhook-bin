using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebhookBin.Infrastructure.Persistence;

namespace WebhookBin.Infrastructure;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAndConfigureDbContext(string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(connectionString);
            });
            
            return services;
        }
    }
}