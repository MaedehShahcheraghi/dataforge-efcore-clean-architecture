using DataForge.Domain.Common.Exceptions;

namespace DataForge.Domain.Exceptions
{
    public sealed class InvalidProductNameException(string reason) : DomainException(
        "catalog.product.invalid_name",
        reason);
}
