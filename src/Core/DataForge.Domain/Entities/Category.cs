using System.ComponentModel.DataAnnotations;
using DataForge.Domain.Common.Entities;
using DataForge.Domain.Eunms;
using DataForge.Domain.Exceptions;

namespace DataForge.Domain.Entities
{
    public class Category : Entity<int>
    {
        private readonly List<ProductCategory> _productCategories = [];

        private Category()
        {
        }

        public Guid PublicId { get; set; }

        [MaxLength(100, ErrorMessage = "Category name cannot exceed {1} characters.")]
        public string Name { get; private set; } = null!;

        [MaxLength(100, ErrorMessage = "Slug cannot exceed {1} characters.")]

        public string Slug
        {
            get;
            set;
        } = null!;

        public bool IsActive { get; set; }

        public void Rename(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                throw new BaseDomainExceptions(
                    ErrorMessages.CategoryInvalidName,
                    "Category name cannot be empty.");
            }

            Name = name.Trim();
        }

        public void ChangeSlug(string slug)
        {
            if (!string.IsNullOrEmpty(slug))
            {
                throw new BaseDomainExceptions(ErrorMessages.CategoryInvalidSlug,
                    "Category slug cannot be empty.");
            }

            Slug = slug.Trim().ToLowerInvariant();
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        internal void AttachProduct(ProductCategory productCategory) => _productCategories.Add(productCategory);
    }
}
