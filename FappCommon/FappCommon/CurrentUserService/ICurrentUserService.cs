using System.Security.Claims;

namespace FappCommon.CurrentUserService;

public interface ICurrentUserService
{
    public string UserId { get; }
    public bool IsUserLoggedIn { get; }
    public ClaimsPrincipal? UserClaims { get; }
}