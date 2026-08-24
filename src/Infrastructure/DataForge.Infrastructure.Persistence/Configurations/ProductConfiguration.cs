using DataForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataForge.Infrastructure.Persistence.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id)
                .ValueGeneratedOnAdd();

            builder.Property(product => product.PublicId)
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasIndex(product => product.PublicId)
                .IsUnique()
                .HasDatabaseName("UX_Products_PublicId");

            builder.Property(product => product.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(product => product.Sku)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsRequired();

            builder.HasIndex(product => product.Sku)
                .IsUnique()
                .HasDatabaseName("UX_Products_Sku");

            builder.Property(product => product.Description)
                .HasMaxLength(2_000);

            builder.Property(product => product.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(product => product.IsActive)
                .IsRequired();

            builder.Navigation(product => product.ProductCategories)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
