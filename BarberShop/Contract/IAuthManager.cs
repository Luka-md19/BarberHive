using BarberShop.Models.User;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Contract
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> ValidateUser(ApiUserDto userDto);
        Task<AuthResponseDto> Login(LoginDto login);
        Task<IEnumerable<IdentityError>> CreateAdminUser(ApiUserDto userDto);

        Task<string> CreateRefreshToken();
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);

        Task<AuthResponseDto> CreateAdminAccountAsync(ApiUserDto userDto);
    }
}
