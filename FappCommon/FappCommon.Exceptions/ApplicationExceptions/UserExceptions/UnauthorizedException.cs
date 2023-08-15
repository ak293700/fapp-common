using FappCommon.Exceptions.ApplicationExceptions.UserExceptions.Base;

namespace FappCommon.Exceptions.ApplicationExceptions.UserExceptions;

public class UnauthorizedException : UserException
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public static UnauthorizedException Instance =>
        new("This action require authorization you don't have.");
}