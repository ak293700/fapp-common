using FappCommon.Exceptions.ApplicationExceptions.UserExceptions.Base;

namespace FappCommon.Exceptions.ApplicationExceptions.UserExceptions;

public class NotAuthorizedApplicationException : UserException
{
    public NotAuthorizedApplicationException(string message) : base(message)
    {
    }

    public static NotAuthorizedApplicationException Instance =>
        new("This action require authorization you don't have.");
}