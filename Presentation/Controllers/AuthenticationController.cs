
using Shared.DTOS.IdentityDTOS;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServicesManager _servicesManager) : ApiController
    {
        #region Login  
        [HttpPost("LogIn")]
        public async Task<ActionResult<UserResultDto>> LogInAsync(LoginDto loginDto)
            => Ok(await _servicesManager.AuthenticationService.LoginAsync(loginDto));
        #endregion
        #region Register 
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto)
            => Ok(await _servicesManager.AuthenticationService.RegisterAsync(registerDto));
        #endregion

    }
}
