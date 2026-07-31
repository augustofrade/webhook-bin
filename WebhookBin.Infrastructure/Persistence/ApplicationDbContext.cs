using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebhookBin.Domain.BinRequests;
using WebhookBin.Domain.Bins;
using WebhookBin.Domain.Common;

namespace WebhookBin.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Bin> Bins { get; private init; }
    public DbSet<BinRequest> BinRequests { get; private init; }
    
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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleUpdateDates();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void HandleUpdateDates()
    {
        var entities = ChangeTracker.Entries()
            .Where(e => e is { Entity: Entity, State: EntityState.Modified or EntityState.Added });
        
        foreach (var entity in entities)
        {
            ((Entity)entity.Entity).ModifiedAt = DateTimeOffset.UtcNow;
        }
    }
}