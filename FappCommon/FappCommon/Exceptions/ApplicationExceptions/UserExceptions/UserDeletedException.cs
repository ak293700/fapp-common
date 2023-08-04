using FappCommon.Exceptions.ApplicationExceptions.UserExceptions.Base;

namespace FappCommon.Exceptions.ApplicationExceptions.UserExceptions;

public class UserDeletedException : UserException
{
    public UserDeletedException(string message) : base(message)
    {
    }
    
    public static UserDeletedException Instance => new ("The user has been deleted.");

}