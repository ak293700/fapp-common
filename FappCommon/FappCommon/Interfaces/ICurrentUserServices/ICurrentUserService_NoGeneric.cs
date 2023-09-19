using System.Security.Claims;

namespace FappCommon.Interfaces.ICurrentUserServices;

public interface ICurrentUserService
{
    public string UserIdAsString { get; }
    public bool IsUserLoggedIn { get; }
    public ClaimsPrincipal? UserClaims { get; }
}