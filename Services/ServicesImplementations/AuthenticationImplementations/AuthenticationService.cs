using Domain.Entities.IdentityModule;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Common;
using Shared.DTOS.IdentityDTOS;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.ServicesImplementations.IAuthenticationImplementations
{
    public class AuthenticationService(
        UserManager<User> _userManager , IOptions<JwtOptions> _options
        ) : IAuthenticationService
    {
        #region LoginAsync
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            // Check if the Email  exists or not
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnauthorizedException();
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid) throw new UnauthorizedException();
            return new UserResultDto(user.DisplayName, await CreateTokenAync(user), user.Email!);


        }
        #endregion

        #region RegisterAsync
        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDto(user.DisplayName, await CreateTokenAync(user), user.Email!);
        }
        #endregion

        #region CreateTokenAync [Handle Mothod]
        private async Task<string> CreateTokenAync(User user)
        {
            var jwtOptions = _options.Value;
            #region Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email!),
            };
            // create claims for the user roles
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            #endregion
            #region create a secret Key 
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            // create signing credentials
            var signingCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            #endregion
            #region create a token

            var token = new JwtSecurityToken(
                 issuer: jwtOptions.Issuer,
                 audience: jwtOptions.Audience,
                 claims: claims,
                 expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationDays),
                 signingCredentials: signingCredentials

                 );

            #endregion
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
