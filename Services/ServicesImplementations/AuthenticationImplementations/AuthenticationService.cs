using Domain.Entities.IdentityModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Common;
using Shared.DTOS.IdentityDTOS;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Address = Domain.Entities.IdentityModule.Address;

namespace Services.ServicesImplementations.IAuthenticationImplementations
{
    public class AuthenticationService(
        UserManager<User> _userManager , 
        IOptions<JwtOptions> _options,
        IMapper _mapper
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

        #region GetCurrentUser
        public async Task<UserResultDto> GetCurrentUserAsync(string userEmail)
        {
           var user = await _userManager.FindByEmailAsync(userEmail) ?? throw new UserNotFoundException(userEmail);
           return new UserResultDto(user.DisplayName, await CreateTokenAync(user), user.Email!);
        }
        #endregion
        #region CheckEmailExist
        public async Task<bool> CheckEmailExistsAsync(string userEmail)
        {
            var user =await _userManager.FindByEmailAsync(userEmail);
            return user != null;
        } 
        #endregion

        #region Get&Update Email
        public async Task<AddressDtos> GetUserAddressAsync(string userEmail)
        {
            // incude the user address and get the user by email
            var userAddress =  await _userManager.Users.Include(u => u.Address).
                FirstOrDefaultAsync(u => u.Email == userEmail) ?? throw new UserNotFoundException(userEmail);
            //  map the address to addressDtos and return it
            return _mapper.Map<AddressDtos>(userAddress.Address);
        }
        public async Task<AddressDtos> UpdateUserAddressAsync(string userEmail, AddressDtos addressDtos)
        {
            // incude the user address and get the user by email
            var userAddress = await _userManager.Users.Include(u => u.Address).
                FirstOrDefaultAsync(u => u.Email == userEmail) ?? throw new UserNotFoundException(userEmail);
            // if i have an address i will update it else i will create a new one and map it to the user address
            if (userAddress.Address != null)
            {
                userAddress.Address.FirstName = addressDtos.FirstName; 
                userAddress.Address.LastName = addressDtos.LastName;
                userAddress.Address.Country = addressDtos.Country;
                userAddress.Address.Street = addressDtos.Street;
                userAddress.Address.City = addressDtos.City;


            }
            else
            {
                // create a new address and map it to the user address
                var address = _mapper.Map<Address>(addressDtos);
                userAddress.Address = address;
            }
            await _userManager.UpdateAsync(userAddress);
            return  _mapper.Map<AddressDtos>(userAddress.Address) ;

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
