using BookLending.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static BookLending.Repositories.AuthenticationRepository;

namespace BookLending.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterInfo regInfo)
        {
            int result = _authenticationService.Register(regInfo);

            switch (result)
            {
                case -1: return BadRequest("Invalid Email/password");
                case 0: return BadRequest("something went wrong");
                default: return Ok(result);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginInfo logInfo)
        {
            int result = _authenticationService.Login(logInfo.Email, logInfo.Password);

            switch (result)
            {
                case -1: return Unauthorized("Invalid Email/password");
                case 0: return BadRequest("something went wrong");
                default: return Ok(result);
            }
        }

    }
}
