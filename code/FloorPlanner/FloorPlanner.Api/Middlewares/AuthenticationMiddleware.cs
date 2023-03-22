using FloorPlanner.Bll.UserProfile;
using FloorPlanner.Common;
using System.Globalization;
using System.Security.Claims;

namespace FloorPlanner.Api.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, IUserProfileService userProfileService)
    {
        if (context.User.Identity is ClaimsIdentity user && user.IsAuthenticated &&
            !string.IsNullOrEmpty(user.Name) && !context.User.HasClaim(x => x.Type == CustomClaimTypes.UserId))
        {
            var userProfile = await userProfileService.GetOrCreateUserProfileAsync(user.Name);
            user.AddClaim(new Claim(CustomClaimTypes.UserId, userProfile.Id.ToString(CultureInfo.InvariantCulture)));
        }

        await _next(context);
    }
}