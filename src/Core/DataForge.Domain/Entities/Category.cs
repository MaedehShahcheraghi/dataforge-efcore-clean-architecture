using System.ComponentModel.DataAnnotations;
using DataForge.Domain.Common.Entities;
using DataForge.Domain.Enums;
using DataForge.Domain.Exceptions;

namespace DataForge.Domain.Entities
{
    public class Category : Entity<long>
    {
        private readonly List<ProductCategory> _productCategories = [];

        public IReadOnlyCollection<ProductCategory> ProductCategories =>
            _productCategories;
        private Category()
        {
        }

        public Category(string name, string slug)
        {
            PublicId = Guid.NewGuid();
            IsActive = true;

            Rename(name);
            ChangeSlug(slug);
        }


        public Guid PublicId { get; set; }

        [MaxLength(100, ErrorMessage = "Category name cannot exceed {1} characters.")]
        public string Name { get; private set; } = null!;

        [MaxLength(100, ErrorMessage = "Slug cannot exceed {1} characters.")]

        public string Slug
        {
            get;
            protected set;
        } = null!;

        public bool IsActive { get; protected set; }

        public void Rename(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                throw new DomainRuleException(
                    DomainErrorCode.CategoryInvalidName,
                    "Category name cannot be empty.");
            }

            Name = name.Trim();
        }

        public void ChangeSlug(string slug)
        {
            if (!string.IsNullOrEmpty(slug))
            {
                throw new DomainRuleException(DomainErrorCode.CategoryInvalidSlug,
                    "Category slug cannot be empty.");
            }

            Slug = slug.Trim().ToLowerInvariant();
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        internal void AttachProduct(ProductCategory productCategory) => _productCategories.Add(productCategory);
    }
}
