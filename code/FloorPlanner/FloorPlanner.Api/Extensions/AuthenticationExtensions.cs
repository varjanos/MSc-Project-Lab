using Microsoft.AspNetCore.Authentication.Negotiate;

namespace FloorPlanner.Api.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddWindowsAuthenticationAndAuthorization(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

        serviceCollection.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
            options.AddPolicy("UsagePolicy", policy =>
            {
                policy.RequireRole(configuration.GetValue<string>("RequiredGroup"));
                policy.Build();
            });
        });

        return serviceCollection;
    }
}