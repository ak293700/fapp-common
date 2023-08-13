using FappCommon.Exceptions.ApplicationExceptions.UserExceptions.Base;

namespace FappCommon.Exceptions.ApplicationExceptions.UserExceptions;

public class NotLoggedInApplicationException : UserException
{
    public NotLoggedInApplicationException(string message) : base(message)
    {
    }

    public static NotLoggedInApplicationException Instance => new("No user logged in.");
}