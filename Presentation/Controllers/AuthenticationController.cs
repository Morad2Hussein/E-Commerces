
using Microsoft.AspNetCore.Authorization;
using Shared.DTOS.IdentityDTOS;
using Shared.DTOS.OrderDTOS;
using System.Security.Claims;

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
        #region Check Email Exist
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync(string email)
            => Ok(await _servicesManager.AuthenticationService.CheckEmailExistsAsync(email));
        #endregion
        #region Get Current User
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _servicesManager.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(user);
        }
        #endregion
        #region Get User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDtos>> GetUserAddressAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _servicesManager.AuthenticationService.GetUserAddressAsync(email);
            return Ok(address);
        }
        #endregion
        #region Update user Address
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDtos>> UpdateUserAddressAsync(AddressDtos addressDtos)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress = await _servicesManager.AuthenticationService.UpdateUserAddressAsync(email, addressDtos);
            return Ok(updatedAddress);
        }
        #endregion

    }
}
