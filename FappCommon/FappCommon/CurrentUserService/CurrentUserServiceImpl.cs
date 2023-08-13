using System.Security.Claims;
using FappCommon.Exceptions.ApplicationExceptions.UserExceptions;
using FappCommon.Exceptions.ApplicationExceptions.UserExceptions.Base;
using Microsoft.AspNetCore.Http;

namespace FappCommon.CurrentUserService;

/// <summary>
/// Service that manage UserRights.
/// If try to access a propriety while no user are logged it throw a <see cref="UserException.NotLoggedInApplicationExceptions"/>.
/// To check if a user is logged use <see cref="IsUserLoggedIn"/>.
/// Has ot be overriden to custom the ClaimTypes.NameIdentifier.
/// </summary>
public class CurrentUserServiceImpl : ICurrentUserService
{
    private readonly string? _userId = null;
    public string UserId => _userId ?? throw NotLoggedInApplicationException.Instance;
    public bool IsUserLoggedIn { get; init; }
    public ClaimsPrincipal? UserClaims { get; init; }


    public CurrentUserServiceImpl(IHttpContextAccessor httpContextAccessor)
    {
        UserClaims = httpContextAccessor.HttpContext?.User;
        IsUserLoggedIn = UserClaims?.Identity?.IsAuthenticated ?? false;
        _userId = UserClaims?.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}