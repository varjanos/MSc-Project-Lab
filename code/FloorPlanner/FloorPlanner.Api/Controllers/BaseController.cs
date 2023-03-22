using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Policy = "UsagePolicy")]
public abstract class BaseController : ControllerBase
{

}