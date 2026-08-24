using DataForge.Domain.Common.Exceptions;
using DataForge.Domain.Enums;

namespace DataForge.Domain.Exceptions
{
    public sealed class DomainRuleException(DomainErrorCode domainErrorCode, string reason) : DomainException(
        domainErrorCode,
        reason);
}
