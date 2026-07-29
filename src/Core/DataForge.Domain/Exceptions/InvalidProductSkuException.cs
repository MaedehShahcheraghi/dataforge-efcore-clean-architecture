using DataForge.Domain.Common.Exceptions;

namespace DataForge.Domain.Exceptions
{
    public sealed class InvalidProductSkuException(string reason)
        : DomainException(
            "catalog.product.invalid_sku",
            reason);
}
