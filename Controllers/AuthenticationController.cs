using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Authorization;
using WebApiJwt.Model;

namespace WebApiJwt
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase 
    {
        private readonly IAuthorizationService _authorization;

        public AuthenticationController(IAuthorizationService authorization)
        {
            _authorization = authorization;
        }

        [HttpPost("login")]
        public IActionResult Authenticate(AuthenticationModel model)
        {
            var secureToken = _authorization.Authenticate(model);
            if (secureToken is null)
                return Unauthorized();
            
            return Ok(secureToken);
        }
    }
}