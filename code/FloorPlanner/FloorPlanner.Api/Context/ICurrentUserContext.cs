namespace FloorPlanner.Api.Context;

public interface ICurrentUserContext : ICurrentUserIdContext
{
    int? CurrentUserId { get; }
}