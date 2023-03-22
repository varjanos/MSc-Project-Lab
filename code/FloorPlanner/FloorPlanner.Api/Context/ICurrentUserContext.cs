using Core.Context.CurrentUserIdContext;

namespace FloorPlanner.Api.Context;

public interface ICurrentUserContext : ICurrentUserIdContext
{
    int? CurrentUserId { get; }
}