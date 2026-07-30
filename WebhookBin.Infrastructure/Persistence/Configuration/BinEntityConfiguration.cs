using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebhookBin.Domain.Bins;

namespace WebhookBin.Infrastructure.Persistence.Configuration;

public class BinEntityConfiguration : IEntityTypeConfiguration<Bin> 
{
    public void Configure(EntityTypeBuilder<Bin> builder)
    {
        builder.HasKey("Id");
        builder.Property<int>("Id")
            .ValueGeneratedOnAdd();
        
        builder.Property(b => b.PublicId)
            .HasConversion(
                v => v.Value,
                v => new BinPublicId(v));
        builder.HasIndex(b => b.PublicId);
    }
}