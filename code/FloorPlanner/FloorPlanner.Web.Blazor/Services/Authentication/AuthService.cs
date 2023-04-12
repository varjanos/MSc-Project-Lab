using Blazored.LocalStorage;
using FloorPlanner.Web.Blazor.Client;
using FloorPlanner.Web.Blazor.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace FloorPlanner.Web.Blazor.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthenticationClient _authenticationClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(
        HttpClient httpClient,
        IAuthenticationClient authenticationClient,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationClient = authenticationClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<LoginResponse> LoginAsync(LoginModel loginModel)
    {
        var loginResponse = await _authenticationClient.LoginAsync(loginModel);

        if(loginResponse == null)
        {
            return new LoginResponse { Success = false, Error = "Unable to retrive token from server!", };
        }

        if(!loginResponse.Success)
        {
            return loginResponse;
        }

        await _localStorage.SetItemAsync("authToken", loginResponse.Token);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.UserName);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponse.Token);

        return loginResponse;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterModel registerModel)
        => await _authenticationClient.RegisterAsync(registerModel);
    
}
