using FloorPlanner.Transfer.Plan;

namespace FloorPlanner.Bll.Plan;

public interface IPlanService
{
    Task<List<PlanDto>> GetMyPlansAsync(int? currentUserId);
}
