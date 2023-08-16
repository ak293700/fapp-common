using FappCommon.Interfaces.ICurrentUserServices;
using Microsoft.AspNetCore.Http;

namespace FappCommon.Implementations.ICurrentUserServices;

public class CurrentUserServiceIntImpl : CurrentUserServiceBaseImpl<int>, ICurrentUserServiceInt
{
    public CurrentUserServiceIntImpl(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected override int ConvertUserIdAsStringToUserId()
    {
        return int.Parse(UserIdAsString);
    }
}