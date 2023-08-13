using FappCommon.Exceptions.ApplicationExceptions.UserExceptions.Base;

namespace FappCommon.Exceptions.ApplicationExceptions.UserExceptions;

public class SameUserException : UserException
{
    public SameUserException(string message) : base(message)
    {
    }

    public static SameUserException Instance => new("User is the same. Should be different.");
}