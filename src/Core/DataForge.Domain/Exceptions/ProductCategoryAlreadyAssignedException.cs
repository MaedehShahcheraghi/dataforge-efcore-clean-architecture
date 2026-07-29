using DataForge.Domain.Common.Exceptions;

namespace DataForge.Domain.Exceptions
{
    public sealed class ProductCategoryAlreadyAssignedException(
        Guid productPublicId,
        Guid categoryPublicId)
        : DomainException(
            "catalog.product.category_already_assigned",
            $"Category '{categoryPublicId}' is already assigned " +
            $"to product '{productPublicId}'.");
}
