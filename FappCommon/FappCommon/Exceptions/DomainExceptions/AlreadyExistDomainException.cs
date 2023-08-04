using FappCommon.Exceptions.DomainExceptions.Base;

namespace FappCommon.Exceptions.DomainExceptions;

public class AlreadyExistDomainException : DomainException
{
    public AlreadyExistDomainException(string message) : base(message)
    {}

    public static AlreadyExistDomainException Instance => new("This resource already exist");
}