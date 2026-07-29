namespace DataForge.Domain.Common.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(
            string code,
            string message)
            : base(message)
        {
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
