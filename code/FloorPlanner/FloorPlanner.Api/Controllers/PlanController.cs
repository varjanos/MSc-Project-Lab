using FloorPlanner.Api.Context;
using FloorPlanner.Bll.Plan;
using FloorPlanner.Transfer.Plan;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanController : ControllerBase
{
    private readonly IPlanService _planService;
    private readonly ICurrentUserContext _currentUserContext;

    public PlanController(IPlanService planService, ICurrentUserContext currentUserContext)
    {
        _planService = planService;
        _currentUserContext = currentUserContext;
    }

    [HttpGet]
    public async Task<List<PlanDto>> GetMyPlansAsync()
    {
        var currentUserId = _currentUserContext.CurrentUserId;
        
        return await _planService.GetMyPlansAsync(currentUserId);
    }
}
