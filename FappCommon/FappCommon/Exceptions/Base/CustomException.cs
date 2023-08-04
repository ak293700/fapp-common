namespace FappCommon.Exceptions.Base;

public abstract class CustomException : Exception
{
    public CustomException(string message) : base(message)
    {}
}