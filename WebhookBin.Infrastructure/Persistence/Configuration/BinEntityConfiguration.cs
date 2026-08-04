using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebhookBin.Domain.BinRequests;
using WebhookBin.Domain.Bins;
using WebhookBin.Infrastructure.Extensions;

namespace WebhookBin.Infrastructure.Persistence.Configuration;

public class BinEntityConfiguration : IEntityTypeConfiguration<Bin> 
{
    public void Configure(EntityTypeBuilder<Bin> builder)
    {
        builder.HasAutoIncrementedPrimaryKey();
        
        builder.Property(b => b.PublicId)
            .HasConversion(
                v => v.Value,
                v => new BinPublicId(v));
        builder.HasIndex(b => b.PublicId);
        
        builder.Property(b => b.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Navigation(b => b.Requests)
            .HasField("_requests")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasMany<BinRequest>(b => b.Requests)
            .WithOne()
            .HasForeignKey("BinId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}