using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Extensions;
using Core.Translation.Dal.Entities;
using FloorPlanner.Bll.Extensions;
using FloorPlanner.Dal.Context;
using FloorPlanner.Transfer.UserProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using UserProfileEntity = FloorPlanner.Dal.Entities.UserProfile;

namespace FloorPlanner.Bll.UserProfile;

public class UserProfileService : IUserProfileService
{
    public const int InitialNumberOfCreateUserProfileRequests = 1;
    public const int MaximumNumberOfCreateUserProfileRequests = 1;
    public const string UserProfilesNamePrefix = "UserProfiles_";

    private static readonly ConcurrentDictionary<string, SemaphoreSlim> UserProfileCreateSemaphores = new();

    private readonly FloorPlannerDbContext _dbContext;
    private readonly ILogger<UserProfileService> _logger;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;

    public UserProfileService(FloorPlannerDbContext context, ILogger<UserProfileService> logger, IMapper mapper, IMemoryCache memoryCache)
    {
        _dbContext = context;
        _logger = logger;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    public async Task<UserProfileDto> GetOrCreateUserProfileAsync(string fullADUserName)
    {
        (var domainName, var userName) = GetDomainAndUserNameFromADUserName(fullADUserName);

        var cacheName = UserProfilesNamePrefix + fullADUserName.ToLowerInvariant();

        return await _memoryCache.GetOrCreateWithErrorHandlingAsync(
             cacheName,
             async _ =>
             {
                 // allow only 1 concurrent call to an AD user at a time
                 var semaphore = UserProfileCreateSemaphores.GetOrAdd(
                     cacheName,
                     _ => new SemaphoreSlim(InitialNumberOfCreateUserProfileRequests, MaximumNumberOfCreateUserProfileRequests));

                 try
                 {
                     await semaphore.WaitAsync();

                     var userProfile = await _dbContext.UserProfile
                       .Where(upr => upr.Domain == domainName && upr.UserName == userName)
                       .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
                       .SingleOrDefaultAsync();

                     if (userProfile == null)
                     {
                         var language = await _dbContext.Language.FirstOrDefaultAsync();
                         var entity = await CreateUserProfileEntityAsync(fullADUserName, language);
                         userProfile = _mapper.Map<UserProfileDto>(entity);
                     }

                     _logger.LogTrace("MemoryCache entry UserProfileId: {UserProfileId} created.", userProfile.Id);
                     return userProfile;
                 }
                 finally
                 {
                     // If the WaitAsync OperationCancelledException is thrown,
                     // no longer need to use the Release
                     if (semaphore.CurrentCount < 1)
                     {
                         semaphore.Release();
                     }
                 }
             },
             new MemoryCacheEntryOptions
             {
                 Priority = CacheItemPriority.NeverRemove,
             });
    }

    public async Task<UserProfileDto> GetUserProfileAsync(int userProfileId) => await _dbContext.UserProfile
            .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
            .SingleAsync(u => u.Id == userProfileId);

    public async Task SetLanguageForUserProfileAsync(int userProfileId, string language)
    {
        var userProfile = await _dbContext.UserProfile
            .SingleAsync(u => u.Id == userProfileId);

        if (userProfile.LanguageName == language)
        {
            return;
        }

        userProfile.LanguageName = language;
        await _dbContext.SaveChangesAsync();

        _logger.LogUpdateItem(nameof(SetLanguageForUserProfileAsync), userProfile.Id);

        InvalidateCache(userProfile);
    }

    private static (string DomainName, string UserName) GetDomainAndUserNameFromADUserName(string fullADUserName)
    {
        if (string.IsNullOrEmpty(fullADUserName))
        {
            throw new ArgumentNullException(nameof(fullADUserName));
        }

        if (fullADUserName?.Split(@"\").Length != 2)
        {
            throw new ArgumentException(nameof(GetDomainAndUserNameFromADUserName));
        }

        var domainName = fullADUserName.Split(@"\")[0];

        var userName = fullADUserName.Split(@"\")[1];

        return (domainName, userName);
    }

    /// <summary>
    /// Creates UserProfile Entity with the given username.
    /// </summary>
    /// <param name="fullADUserName">User's full AD username, Format: 'domain\username'.</param>
    /// <returns>The created UserProfile entity</returns>
    private async Task<UserProfileEntity> CreateUserProfileEntityAsync(string fullADUserName, Language language)
    {
        (var domainName, var userName) = GetDomainAndUserNameFromADUserName(fullADUserName);

        var userProfile = await _dbContext.UserProfile
            .Where(upr => upr.Domain == domainName && upr.UserName == userName)
            .SingleOrDefaultAsync();

        if (userProfile != null)
        {
            return userProfile;
        }

        userProfile = new UserProfileEntity
        {
            Domain = domainName,
            UserName = userName,
            Language = language,
        };

        _dbContext.UserProfile.Add(userProfile);
        await _dbContext.SaveChangesAsync();

        _logger.LogCreateItem(nameof(CreateUserProfileEntityAsync), userProfile.Id);

        return userProfile;
    }

    private void InvalidateCache(UserProfileEntity userProfile)
    {
        var cacheName = string.Concat(UserProfilesNamePrefix, userProfile.Domain.ToLowerInvariant(), @"\", userProfile.UserName.ToLowerInvariant());
        _memoryCache.Invalidate(cacheName);
        _logger.LogTrace("MemoryCache entry UserProfileId: {UserProfileId} deleted.", userProfile.Id);
    }
}