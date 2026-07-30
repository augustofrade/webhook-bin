using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebhookBin.Domain.Common;

namespace WebhookBin.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.EnableSensitiveDataLogging();
        
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    private void HandleUpdateDates()
    {
        var entities = ChangeTracker.Entries().Where(e => e is { Entity: Entity, State: EntityState.Modified or EntityState.Added }).ToList();
        foreach (var entity in entities)
        {
            ((Entity)entity.Entity).ModifiedAt = DateTime.UtcNow;
        }
    }
}