namespace FappCommon.Exceptions.Base;

public class CustomException : Exception
{
    public CustomException(string message) : base(message)
    {
    }
}