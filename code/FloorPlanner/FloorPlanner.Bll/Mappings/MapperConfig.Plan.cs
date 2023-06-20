using AutoMapper;
using FloorPlanner.Transfer.Plan;

namespace FloorPlanner.Bll.Mappings;

public static partial class MapperConfig
{
    private static IMapperConfigurationExpression ConfigurePlan(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<FloorPlanner.Dal.Entities.Plan, PlanDto>();
        return cfg;
    }
}
