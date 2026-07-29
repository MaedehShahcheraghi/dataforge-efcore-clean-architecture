using DataForge.Domain.Common.Exceptions;
using DataForge.Domain.Eunms;

namespace DataForge.Domain.Exceptions
{
    public sealed class BaseDomainExceptions(ErrorMessages errorCode, string reason) : DomainException(
        errorCode,
        reason);
}
