using Core.Context.RequestContext;

namespace FloorPlanner.Test.Base;

public class TestRequestContext : IRequestContext
{
    public string CurrentUserAd => "Tester";
    public string RequestId => string.Empty;
}