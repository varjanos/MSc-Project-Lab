using FloorPlanner.Web.Blazor.Client;
using FloorPlanner.Web.Blazor.Constants;
using FloorPlanner.Web.Blazor.Services.DisplayError;
using FloorPlanner.Web.Blazor.Services.Progress;
using FloorPlanner.Web.Blazor.Services.UserProfile;

namespace FloorPlanner.Web.Blazor;

public static class ServiceStartup
{
    public static void AddApiClientServices(this IServiceCollection services)
    {
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
        .CreateClient(HttpClientNames.ApiHttpClientName));

        services.AddHttpClient<ITranslationClient, TranslationClient>(HttpClientNames.ApiHttpClientName);
        services.AddHttpClient<IUserProfileClient, UserProfileClient>(HttpClientNames.ApiHttpClientName);
    }

    public static void AddClientServices(this IServiceCollection services)
    {
        services.AddSingleton<IProgressClientService, ProgressClientService>();
        services.AddSingleton<IDisplayErrorClientService, DisplayErrorClientService>();
        services.AddSingleton<IUserProfileClientService, UserProfileClientService>();
    }
}
