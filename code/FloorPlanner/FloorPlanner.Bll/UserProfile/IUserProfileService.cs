using FloorPlanner.Transfer.Authentication;
using FloorPlanner.Transfer.AuthenticationResponse;
using FloorPlanner.Transfer.UserProfile;

namespace FloorPlanner.Bll.UserProfile;

public interface IUserProfileService
{
    Task<LoginResponse> LoginUserAsync(LoginModel loginModel);
    Task<RegisterResponse> RegisterUserAsync(RegisterModel registerModel);

    Task<UserProfileDto> GetUserProfileAsync(int userProfileId);
}