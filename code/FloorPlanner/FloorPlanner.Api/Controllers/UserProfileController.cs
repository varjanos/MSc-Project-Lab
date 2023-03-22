using FloorPlanner.Api.Context;
using FloorPlanner.Bll.UserProfile;
using FloorPlanner.Transfer.UserProfile;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanner.Api.Controllers;

public class UserProfileController : BaseController
{
    private readonly IUserProfileService _userProfileService;
    private readonly ICurrentUserContext _currentUserContext;

    public UserProfileController(IUserProfileService userProfileService, ICurrentUserContext currentUserContext)
    {
        _userProfileService = userProfileService;
        _currentUserContext = currentUserContext;
    }

    [HttpGet]
    public async Task<UserProfileDto> GetUserProfileDtoAsync()
    {
        if (!_currentUserContext.CurrentUserId.HasValue)
        {
            throw new InvalidOperationException();
        }

        return await _userProfileService.GetUserProfileAsync(_currentUserContext.CurrentUserId.Value);
    }

    [HttpPut]
    public async Task SetUserLanguageAsync(string language)
    {
        if (!_currentUserContext.CurrentUserId.HasValue)
        {
            throw new InvalidOperationException();
        }

        await _userProfileService.SetLanguageForUserProfileAsync(_currentUserContext.CurrentUserId.Value, language);
    }
}