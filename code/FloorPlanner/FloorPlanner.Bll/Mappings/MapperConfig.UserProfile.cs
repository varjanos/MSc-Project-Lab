using AutoMapper;
using FloorPlanner.Transfer.UserProfile;
using UserProfileEntity = FloorPlanner.Dal.Entities.UserProfile;

namespace FloorPlanner.Bll.Mappings;

public static partial class MapperConfig
{
    private static IMapperConfigurationExpression ConfigureUserProfile(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<UserProfileEntity, UserProfileDto>()
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.LanguageName));
        return cfg;
    }
}