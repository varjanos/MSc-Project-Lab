using FloorPlanner.Transfer.UserProfile;

namespace FloorPlanner.Bll.UserProfile;

public interface IUserProfileService
{
    Task<UserProfileDto> GetOrCreateUserProfileAsync(string fullADUserName);

    Task<UserProfileDto> GetUserProfileAsync(int userProfileId);

    Task SetLanguageForUserProfileAsync(int userProfileId, string language);
}