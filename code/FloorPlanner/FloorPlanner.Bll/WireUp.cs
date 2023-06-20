using FloorPlanner.Bll.Plan;
using FloorPlanner.Bll.UserProfile;
using Microsoft.Extensions.DependencyInjection;

namespace FloorPlanner.Bll;

public static class WireUp
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IPlanService, PlanService>();
    }
}