using System.Security.Claims;
using FappCommon.Exceptions.ApplicationExceptions.UserExceptions;
using FappCommon.Exceptions.ApplicationExceptions.UserExceptions.Base;
using FappCommon.Interfaces.ICurrentUserServices;
using Microsoft.AspNetCore.Http;

namespace FappCommon.Implementations.ICurrentUserServices;

/// <summary>
/// Service that manage UserRights.
/// If try to access a propriety while no user are logged it throw a <see cref="UserException.NotLoggedInApplicationExceptions"/>.
/// To check if a user is logged use <see cref="IsUserLoggedIn"/>.
/// Has ot be overriden to custom the ClaimTypes.NameIdentifier.
/// </summary>
public abstract class CurrentUserServiceBaseImpl<T> : ICurrentUserService<T>
{
    // ReSharper disable once InconsistentNaming
    private T? _userId { get; set; }
    public T UserId => _userId ??= ConvertUserIdAsStringToUserId() ?? throw NotLoggedInException.Instance;


    private readonly string? _userIdAsString = null;
    public string UserIdAsString => _userIdAsString ?? throw NotLoggedInException.Instance;

    public bool IsUserLoggedIn { get; }
    public ClaimsPrincipal? UserClaims { get; }


    protected CurrentUserServiceBaseImpl(IHttpContextAccessor httpContextAccessor)
    {
        UserClaims = httpContextAccessor.HttpContext?.User;
        IsUserLoggedIn = UserClaims?.Identity?.IsAuthenticated ?? false;

        _userIdAsString = UserClaims?.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    protected abstract T? ConvertUserIdAsStringToUserId();
}