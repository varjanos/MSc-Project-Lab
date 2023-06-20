using FloorPlanner.Bll.UserProfile;
using FloorPlanner.Transfer.Authentication;
using FloorPlanner.Transfer.AuthenticationResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanner.Api.Controllers;

public class AuthenticationController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;

    public AuthenticationController(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<LoginResponse> LoginAsync([FromBody] LoginModel loginModel)
    {
        return await _userProfileService.LoginUserAsync(loginModel);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<RegisterResponse> RegisterAsync([FromBody]RegisterModel registerModel)
    {
        return await _userProfileService.RegisterUserAsync(registerModel);
    }
}
