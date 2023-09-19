namespace FappCommon.Interfaces.ICurrentUserServices;

public interface ICurrentUserService<out T> : ICurrentUserService
{
    public T UserId { get; }
}