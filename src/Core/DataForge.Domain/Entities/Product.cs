using DataForge.Domain.Common.Entities;
using DataForge.Domain.Eunms;
using DataForge.Domain.Exceptions;

namespace DataForge.Domain.Entities
{
    public sealed class Product : AuditableEntity<long>
    {
        private const int MaximumNameLength = 200;
        private const int MaximumSkuLength = 64;
        private const int MaximumDescriptionLength = 2_000;

        private readonly List<ProductCategory> _productCategories = [];

        private Product()
        {
        }

        public Product(string name, string sku, decimal price)
        {
            PublicId = Guid.NewGuid();
            IsActive = true;

            Rename(name);
            ChangeSku(sku);
            ChangePrice(price);
        }


        public Guid PublicId { get; private set; }

        public string Name { get; private set; } = null!;

        public string Sku { get; private set; } = null!;

        public string? Description { get; private set; }

        public decimal Price { get; private set; }

        public bool IsActive { get; private set; }


        public IReadOnlyCollection<ProductCategory> ProductCategories =>
            _productCategories;

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BaseDomainExceptions(
                    ErrorMessages.ProductInvalidName,
                    "Product name cannot be empty.");
            }

            var normalizedName = name.Trim();

            if (normalizedName.Length > MaximumNameLength)
            {
                throw new BaseDomainExceptions(
                    ErrorMessages.ProductInvalidName,
                    $"Product name cannot exceed {MaximumNameLength} characters.");
            }

            Name = normalizedName;
        }

        public void ChangeSku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new BaseDomainExceptions(
                    ErrorMessages.ProductInvalidSku,
                    $"Product SKU {nameof(sku)} cannot be empty.");
            }

            var normalizedSku = sku.Trim().ToUpperInvariant();

            if (normalizedSku.Length > MaximumSkuLength)
            {
                throw new BaseDomainExceptions(
                    ErrorMessages.ProductInvalidSku,
                    $"Product SKU cannot exceed {MaximumSkuLength} characters.");
            }

            Sku = normalizedSku;
        }

        public void ChangeDescription(string? description)
        {
            if (description is null)
            {
                Description = null;
                return;
            }

            var normalizedDescription = description.Trim();

            if (normalizedDescription.Length > MaximumDescriptionLength)
            {
                throw new BaseDomainExceptions(ErrorMessages.ProductInvalidDescription,
                    $"Product description cannot exceed {MaximumDescriptionLength} characters.");
            }

            Description = normalizedDescription.Length == 0
                ? null
                : normalizedDescription;
        }

        public void ChangePrice(decimal price)
        {
            if (price < 0)
            {
                throw new BaseDomainExceptions(
                    ErrorMessages.ProductInvalidPrice,
                    "Product price cannot be negative.");
            }

            Price = price;
        }

        public void AddCategory(Category category, int displayOrder = 0)
        {
            ArgumentNullException.ThrowIfNull(category);

            if (displayOrder < 0)
            {
                throw new BaseDomainExceptions(
                    ErrorMessages.ProductInvalidName,
                    "Display order cannot be negative.");
            }

            var alreadyExists = _productCategories.Any(
                productCategory =>
                    productCategory.Category.PublicId == category.PublicId);

            if (alreadyExists)
            {
                return;
            }

            _ = new ProductCategory(this, category, displayOrder);
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        internal void AttachCategory(ProductCategory productCategory) => _productCategories.Add(productCategory);
    }
}
