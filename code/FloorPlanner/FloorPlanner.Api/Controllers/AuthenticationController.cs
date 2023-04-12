using FloorPlanner.Transfer.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanner.Api.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public Task<bool>  LoginAsync(LoginDto dto)
        {
            return Task.FromResult(true);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public Task<bool> RegisterAsync(LoginDto dto)
        {
            return Task.FromResult(true);
        }
    }
}
