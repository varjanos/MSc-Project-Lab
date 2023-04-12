using FloorPlanner.Web.Blazor.Client;

namespace FloorPlanner.Web.Blazor.Services.UserProfile;

public class UserProfileClientService : IUserProfileClientService
{
    private readonly IUserProfileClient _userProfileClient;
    private UserProfileDto _userProfileDto = null!;

    public UserProfileClientService(IUserProfileClient userProfileClient)
    {
        _userProfileClient = userProfileClient ?? throw new ArgumentNullException(nameof(userProfileClient));
    }

    public string GetUserName() => _userProfileDto.UserName;

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        // TODO

        await Task.CompletedTask;

        _userProfileDto = new UserProfileDto { Id = 1, UserName = "Teszt Név", Language = "qwe" };
        //_userProfileDto = await _userProfileClient.GetUserProfileDtoAsync(cancellationToken);
    }
}