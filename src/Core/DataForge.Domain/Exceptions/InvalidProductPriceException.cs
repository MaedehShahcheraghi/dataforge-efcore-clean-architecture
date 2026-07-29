using DataForge.Domain.Common.Exceptions;

namespace DataForge.Domain.Exceptions
{
    public sealed class InvalidProductPriceException(decimal price)
        : DomainException
        (
            "catalog.product.invalid_price",
            $"Product price '{price}' cannot be negative.");
}
