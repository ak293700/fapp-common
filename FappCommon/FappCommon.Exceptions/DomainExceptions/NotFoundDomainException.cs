using FappCommon.Exceptions.DomainExceptions.Base;

namespace FappCommon.Exceptions.DomainExceptions;

public class NotFoundDomainException : DomainException
{
    public NotFoundDomainException(string message) : base(message)
    {
    }

    public static NotFoundDomainException Instance => new("Resource not found");
}