using FappCommon.Exceptions.Base;

namespace FappCommon.Exceptions.DomainExceptions.Base;

public class DomainException : CustomException
{
    public DomainException(string message) : base(message)
    {
    }
}