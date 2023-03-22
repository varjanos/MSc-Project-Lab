using Core.Time;
using FloorPlanner.Bll.UserProfile;
using Microsoft.Extensions.DependencyInjection;

namespace FloorPlanner.Bll
{
    public static class WireUp
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddSingleton<ITimeService, TimeService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
        }
    }
}