using Core.Context.RequestContext;

namespace FloorPlanner.Api.Context;

public class RequestContext : IRequestContext
{
    public string CurrentUserAd { get; }
    public string RequestId { get; }

    public RequestContext(IHttpContextAccessor httpContextAccessor)
    {
        RequestId = Guid.NewGuid().ToString();
        var user = httpContextAccessor.HttpContext?.User;
        if (user?.Identity != null)
        {
            CurrentUserAd = user.Identity.Name;
        }
    }
}