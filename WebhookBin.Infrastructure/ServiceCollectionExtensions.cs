using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebhookBin.Infrastructure.Persistence;
using WebhookBin.Infrastructure.Repositories;

namespace WebhookBin.Infrastructure;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAndConfigureDbContext(string? connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));    
            }
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(connectionString);
            });
            
            return services;
        }

        public IServiceCollection RegisterRepositories()
        {
            services.AddScoped<IBinRepository, BinRepository>();
            
            return services;
        }
    }
}