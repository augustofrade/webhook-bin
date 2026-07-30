using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using WebhookBin.Domain.BinRequests;
using WebhookBin.Infrastructure.Extensions;

namespace WebhookBin.Infrastructure.Persistence.Configuration;

public class BinRequestEntityConfiguration : IEntityTypeConfiguration<BinRequest>
{
    public void Configure(EntityTypeBuilder<BinRequest> builder)
    {
        builder.HasAutoIncrementedPrimaryKey();

        builder.Property(b => b.Method).IsRequired();

        builder.Property(b => b.ReceivedAt).IsRequired();

        builder.Property(b => b.QueryString)
            .HasMaxLength(2048)
            .IsRequired(false);

        builder.Property(b => b.UserAgent)
            .HasMaxLength(1024)
            .IsRequired(false);

        builder.OwnsOne(b => b.Origin, o =>
        {
            o.Property(b => b.Scheme).IsRequired();
            o.Property(b => b.RemoteIp)
                .HasMaxLength(45)
                .IsRequired();
            o.Property(b => b.Host)
                .HasMaxLength(200)
                .IsRequired();
            o.Property(b => b.Raw)
                .HasMaxLength(2048);
        });

        builder.OwnsOne(b => b.Payload, p =>
        {
            p.Property(b => b.ContentType)
                .HasMaxLength(255)
                .IsRequired(false);
            p.Property(b => b.ContentLength)
                .IsRequired(false);
            p.Property(b => b.Body)
                .IsRequired(false);
        });

        builder.Property<int>("BinId").IsRequired();

    }
}