using System.Threading.Tasks;
using Application.Services.Security;
using Blog.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager AuthenticationManager;

        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            this.AuthenticationManager = authenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login([FromBody] AuthenticationDto authentication)
        {
            var loginResult = await this.AuthenticationManager.Login(authentication.Email, authentication.Password);

            ActionResult result = loginResult == LoginResult.Ok ? Ok() : (ActionResult)Unauthorized();

            return result;
        }

        [HttpPost("logout")]
        public async Task<ActionResult<LogoutResult>> Logout()
        {
            await this.AuthenticationManager.Logout();

            return Ok();
        }
    }
}