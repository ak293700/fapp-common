using FappCommon.Interfaces.ICurrentUserServices;
using Microsoft.AspNetCore.Http;

namespace FappCommon.Implementations.ICurrentUserServices;

public class CurrentUserServiceStringImpl : CurrentUserServiceBaseImpl<string>, ICurrentUserServiceString
{
    public CurrentUserServiceStringImpl(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected override string ConvertUserIdAsStringToUserId()
    {
        return UserIdAsString;
    }
}