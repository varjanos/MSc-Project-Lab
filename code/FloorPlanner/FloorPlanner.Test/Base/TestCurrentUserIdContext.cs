using FloorPlanner.Api.Context;

namespace FloorPlanner.Test.Base;

public class TestCurrentUserIdContext : ICurrentUserContext
{
    public int? CurrentUserId => 0;
    public object UserId => CurrentUserId;
}
