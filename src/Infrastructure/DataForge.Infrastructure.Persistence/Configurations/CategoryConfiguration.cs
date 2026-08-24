using DataForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataForge.Infrastructure.Persistence.Configurations
{
    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(category => category.Id);

            builder.Property(category => category.Id)
                .ValueGeneratedOnAdd();

            builder.Property(category => category.PublicId)
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasIndex(category => category.PublicId)
                .IsUnique()
                .HasDatabaseName("UX_Categories_PublicId");

            builder.Property(category => category.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(category => category.Slug)
                .HasMaxLength(120)
                .IsUnicode(false)
                .IsRequired();

            builder.HasIndex(category => category.Slug)
                .IsUnique()
                .HasDatabaseName("UX_Categories_Slug");

            builder.Property(category => category.IsActive)
                .IsRequired();

            builder.Navigation(category => category.ProductCategories)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
