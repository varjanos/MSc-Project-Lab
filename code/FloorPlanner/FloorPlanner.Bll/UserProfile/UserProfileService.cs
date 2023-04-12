using AutoMapper;
using FloorPlanner.Dal.Context;
using FloorPlanner.Transfer.Authentication;
using FloorPlanner.Transfer.AuthenticationResponse;
using FloorPlanner.Transfer.UserProfile;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserProfileEntity = FloorPlanner.Dal.Entities.UserProfile;

namespace FloorPlanner.Bll.UserProfile;

public class UserProfileService : IUserProfileService
{
    private readonly FloorPlannerDbContext _dbContext;
    private readonly ILogger<UserProfileService> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<UserProfileEntity> _userManager;
    private readonly SignInManager<UserProfileEntity> _signInManager;
    private readonly IConfiguration _configuration;

    public UserProfileService(FloorPlannerDbContext context,
        ILogger<UserProfileService> logger,
        IMapper mapper,
        UserManager<UserProfileEntity> userManager,
        SignInManager<UserProfileEntity> signInManager,
        IConfiguration configuration)
    {
        _dbContext = context;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<UserProfileDto> GetUserProfileAsync(int userProfileId)
    {
        await Task.CompletedTask;

        // TODO: Implement GetUserProfileAsync function

        return new UserProfileDto
        {
            Id = 1,
            UserName = "Teszt Név",
            Language = "English",
        };
    }

    public async Task<LoginResponse> LoginUserAsync(LoginModel loginModel)
    {
        var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false);

        if (!result.Succeeded)
        {
            return new LoginResponse { Success = false, Error =  "Invalid email or password." };
        }

        var claims = new[]{new Claim(ClaimTypes.Name, loginModel.UserName)};

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:JwtSecurityKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtOptions:JwtExpiryInDays"]));

        var token = new JwtSecurityToken(
            _configuration["JwtOptions:JwtIssuer"],
            _configuration["JwtOptions:JwtAudience"],
            claims,
            expires: expiry,
            signingCredentials: creds
        );

        return new LoginResponse { Success = true, Token = new JwtSecurityTokenHandler().WriteToken(token) };
    }

    public async Task<RegisterResponse> RegisterUserAsync(RegisterModel registerModel)
    {
        var newUser = new UserProfileEntity
        {
            UserName = registerModel.UserName,
            Email = registerModel.Email,
        };

        var result = await _userManager.CreateAsync(newUser, registerModel.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description);

            return new RegisterResponse { Success = false, Errors = errors, };
        }

        return new RegisterResponse { Success = true, };
    }
}