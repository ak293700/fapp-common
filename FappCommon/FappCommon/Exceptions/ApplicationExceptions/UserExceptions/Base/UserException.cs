using ApplicationException = FappCommon.Exceptions.ApplicationExceptions.Base.ApplicationException;

namespace FappCommon.Exceptions.ApplicationExceptions.UserExceptions.Base;

public class UserException : ApplicationException
{
    public UserException(string message) : base(message)
    {
    }
}