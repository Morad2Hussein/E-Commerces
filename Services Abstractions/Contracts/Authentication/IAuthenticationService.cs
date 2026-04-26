

using Shared.DTOS.OrderDTOS;

namespace Services_Abstractions.Contracts.Authentication
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        // Get the current user from the token
        Task<UserResultDto> GetCurrentUserAsync(string userEmail);
        // check if the email already exists
        Task<bool> CheckEmailExistsAsync(string userEmail);
        // Get the user address
        Task<AddressDtos> GetUserAddressAsync(string userEmail);
        // Update the user address
        Task<AddressDtos> UpdateUserAddressAsync(string userEmail, AddressDtos addressDtos);
    }
}
