namespace Movie.Api.Extensions
{
    public sealed class DomainException : Exception
    {
        public DomainException(string message) 
            : base(message)
        {
            throw new DomainException("Movie is already published.");
            throw new DomainException("Price cannot be negative.");
            throw new DomainException("Hall must contain at least one row.");
        }
    }
}
