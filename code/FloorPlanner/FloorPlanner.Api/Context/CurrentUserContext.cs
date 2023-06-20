using FloorPlanner.Common;
using FloorPlanner.Dal.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FloorPlanner.Api.Context;

public class CurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly FloorPlannerDbContext _dbContext;

    public int? _currentUserId;

    public string _currentUserIdString;
    public int? CurrentUserId
    {
        get
        {
            if (_currentUserId.HasValue)
            {
                return _currentUserId.Value;
            }
            SetCurrentUserId().GetAwaiter();
            return _currentUserId;
        }
    }

    public string CurrentUserIdString
    {
        get
        {
            if (!string.IsNullOrEmpty(_currentUserIdString))
            {
                return _currentUserIdString;
            }
            SetCurrentUserId().GetAwaiter().GetResult();

            return _currentUserIdString;
        }
    }

    public object UserId => CurrentUserId;
    public CurrentUserContext(IHttpContextAccessor httpContextAccessor, FloorPlannerDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }
    private async Task SetCurrentUserId()
    {
        if (_httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity currentUser)
        {
            _currentUserIdString = (await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.UserName.Equals(currentUser.Name)))?.Id;
        }
    }
}