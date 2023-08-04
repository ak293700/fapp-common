using FappCommon.Exceptions.Base;

namespace FappCommon.Exceptions.ApplicationExceptions.Base;

public class ApplicationException : CustomException
{
    public ApplicationException(string message) : base(message)
    {
    }
}