using DataForge.Domain.Common.Extensions;
using DataForge.Domain.Eunms;

namespace DataForge.Domain.Common.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(
            ErrorMessages errorCode,
            string message)
            : base(message)
        {
            var code = errorCode.ToErrorCodeString();
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException(
                    "Exception code cannot be empty.",
                    nameof(code));
            }

            Code = code;
        }

        public string Code { get; }
    }
}
