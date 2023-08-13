using FappCommon.Exceptions.Base;

namespace FappCommon.Exceptions.InfrastructureExceptions.Base;

public class InfrastructureException : CustomException
{
    public InfrastructureException(string message) : base(message)
    {
    }
}