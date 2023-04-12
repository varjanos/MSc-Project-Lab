using FloorPlanner.Common;
using System.Security.Claims;

namespace FloorPlanner.Api.Context;

public class CurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public int? _currentUserId;
    public int? CurrentUserId
    {
        get
        {
            if (_currentUserId.HasValue)
            {
                return _currentUserId.Value;
            }
            SetCurrentUserId();
            return _currentUserId;
        }
    }
    public object UserId => CurrentUserId;
    public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    private void SetCurrentUserId()
    {
        if (_httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity currentUser && currentUser.HasClaim(x => x.Type == CustomClaimTypes.UserId))
        {
            if (int.TryParse(currentUser.Claims.First(x => x.Type.Equals(CustomClaimTypes.UserId, StringComparison.InvariantCultureIgnoreCase)).Value, out var tempCurrentUserId))
            {
                _currentUserId = tempCurrentUserId;
            }
        }
    }
}