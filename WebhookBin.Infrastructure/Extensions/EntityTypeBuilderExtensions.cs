using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebhookBin.Infrastructure.Extensions;

public static class EntityTypeBuilderExtensions
{
    extension<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        public void HasAutoIncrementedPrimaryKey()
        {
            builder.Property<int>("Id")
                .ValueGeneratedOnAdd();
            builder.HasKey("Id");
        }
    }
}