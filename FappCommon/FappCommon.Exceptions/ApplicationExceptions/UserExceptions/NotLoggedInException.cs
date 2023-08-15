using FappCommon.Exceptions.ApplicationExceptions.UserExceptions.Base;

namespace FappCommon.Exceptions.ApplicationExceptions.UserExceptions;

public class NotLoggedInException : UserException
{
    public NotLoggedInException(string message) : base(message)
    {
    }

    public static NotLoggedInException Instance => new("No user logged in.");
}