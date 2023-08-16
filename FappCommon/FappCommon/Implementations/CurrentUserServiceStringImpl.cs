using Microsoft.AspNetCore.Http;

namespace FappCommon.Implementations;

public class CurrentUserServiceStringImpl : CurrentUserServiceBaseImpl<string>
{
    public CurrentUserServiceStringImpl(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected override string ConvertUserIdAsStringToUserId()
    {
        return UserIdAsString;
    }
}