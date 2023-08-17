using FappCommon.Exceptions.Base;

namespace FappCommon.Exceptions.DomainExceptions;

public class OutdatedDomainException : CustomException
{
    public OutdatedDomainException(string message) : base(message)
    {
    }

    public static OutdatedDomainException Instance => new("You are trying to access or modify an outdated resource");
}