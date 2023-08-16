using Microsoft.AspNetCore.Http;

namespace FappCommon.Implementations;

public class CurrentUserServiceIntImpl : CurrentUserServiceBaseImpl<int>
{
    public CurrentUserServiceIntImpl(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected override int ConvertUserIdAsStringToUserId()
    {
        return int.Parse(UserIdAsString);
    }
}