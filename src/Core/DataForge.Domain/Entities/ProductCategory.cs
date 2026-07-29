namespace DataForge.Domain.Entities
{
    public sealed class ProductCategory
    {
        private ProductCategory()
        {
        }

        internal ProductCategory(
            Product product,
            Category category,
            int displayOrder)
        {
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(category);

            if (displayOrder < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(displayOrder),
                    displayOrder,
                    "Display order cannot be negative.");
            }

            Product = product;
            Category = category;
            DisplayOrder = displayOrder;

            product.AttachCategory(this);
            category.AttachProduct(this);
        }

        public long ProductId { get; }

        public Product Product { get; private set; } = null!;

        public long CategoryId { get; }

        public Category Category { get; private set; } = null!;

        public int DisplayOrder { get; private set; }
    }
}
