using AutoMapper;
using Core.Translation.Mappings;

namespace FloorPlanner.Bll.Mappings;

public static partial class MapperConfig
{
    public static IMapper ConfigureAutoMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ConfigureTranslation();
            cfg.ConfigureUserProfile();
        });

        config.AssertConfigurationIsValid();

        return config.CreateMapper();
    }
}