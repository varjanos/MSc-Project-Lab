using FloorPlanner.Web.Blazor.Client;

namespace FloorPlanner.Web.Blazor.Services.Authentication;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginModel loginModel);

    Task<RegisterResponse> RegisterAsync(RegisterModel registerModel);

    Task LogoutAsync();
}
