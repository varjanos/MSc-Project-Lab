using AutoMapper;
using AutoMapper.QueryableExtensions;
using FloorPlanner.Dal.Context;
using FloorPlanner.Transfer.Plan;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanner.Bll.Plan;

public class PlanService : IPlanService
{
    private readonly FloorPlannerDbContext _dbContext;
    private readonly IMapper _mapper;

    public PlanService(FloorPlannerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PlanDto>> GetMyPlansAsync(int? currentUserId)
    {
        if(currentUserId is null)
        {
            return new();
        }

        var id = currentUserId;

        return await _dbContext.Plans.ProjectTo<PlanDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}
