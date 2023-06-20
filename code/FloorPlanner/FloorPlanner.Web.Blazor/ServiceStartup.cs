using FloorPlanner.Web.Blazor.Client;
using FloorPlanner.Web.Blazor.Constants;
using FloorPlanner.Web.Blazor.Services.DisplayError;
using FloorPlanner.Web.Blazor.Services.Progress;

namespace FloorPlanner.Web.Blazor;

public static class ServiceStartup
{
    public static void AddApiClientServices(this IServiceCollection services)
    {
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
        .CreateClient(HttpClientNames.ApiHttpClientName));

        services.AddHttpClient<IUserProfileClient, UserProfileClient>(HttpClientNames.ApiHttpClientName);
        services.AddHttpClient<IAuthenticationClient, AuthenticationClient>(HttpClientNames.ApiHttpClientName);
    }

    public static void AddClientServices(this IServiceCollection services)
    {
        services.AddSingleton<IProgressClientService, ProgressClientService>();
        services.AddSingleton<IDisplayErrorClientService, DisplayErrorClientService>();

        services.AddScoped<IPlanClient, PlanClient>();
    }
}
