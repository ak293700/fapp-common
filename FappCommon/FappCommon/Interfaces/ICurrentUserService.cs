using System.Security.Claims;

namespace FappCommon.Interfaces;

public interface ICurrentUserService<T>
{
    public T UserId { get; }
    public string UserIdAsString { get; }
    public bool IsUserLoggedIn { get; }
    public ClaimsPrincipal? UserClaims { get; }
}