using AutoMapper;
using BarberShop.Contract;
using BarberShop.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberShop.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private ApiUser _user;


        private const string _loginProvider = "BarberShopAPI";
        private const string _refreshToken = "RefreshToken";
        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration, ILogger<AuthManager> logger)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
            this._logger = logger;
        }
        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            _logger.LogInformation("Login attempt for user: {Email}", loginDto.Email);

            _user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (_user == null)
            {
                _logger.LogWarning("No user found with the email: {Email}", loginDto.Email);
                return null;
            }

            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (!isValidUser)
            {
                _logger.LogWarning("Invalid password entered for user: {Email}", loginDto.Email);
                return null;
            }

            // Assuming BarberShopId is correctly set for the user
            var barberShopId = _user.BarberShopId.ToString();

            _logger.LogInformation("Valid credentials for user: {Email}. Generating token. BarberShopId: {BarberShopId}", loginDto.Email, barberShopId);

            var token = await GenerateToken();
            var refreshToken = await CreateRefreshToken();

            _logger.LogInformation("Token and Refresh Token generated for user: {Email} with BarberShopId: {BarberShopId}", loginDto.Email, barberShopId);

            return new AuthResponseDto
            {
                UserId = _user.Id,
                Token = token,
                RefreshToken = refreshToken,
                BarberShopId = barberShopId // Ensure this is set correctly
            };
        }



        public async Task<IEnumerable<IdentityError>> ValidateUser(ApiUserDto userDto)
        {
            var _user = _mapper.Map<ApiUser>(userDto);
            _user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(_user, userDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;

        }
        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Fetch roles and existing claims for the user
            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            // Prepare the claims for the token
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, _user.Email),
        new Claim("uid", _user.Id),
        // Include the BarberShopId as a claim
        new Claim("BarberShopId", _user.BarberShopId.ToString())
    }
            .Union(userClaims) // Add existing claims
            .Union(roleClaims); // Add role claims

            // Create the JWT token with the specified claims
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            // Return the serialized token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<IEnumerable<IdentityError>> CreateAdminUser(ApiUserDto userDto)
        {
            var _user = _mapper.Map<ApiUser>(userDto);
            _user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(_user, userDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "Administrator");
            }

            return result.Errors;
        }

        public async Task<AuthResponseDto> CreateAdminAccountAsync(ApiUserDto userDto)
        {
            var newUser = _mapper.Map<ApiUser>(userDto);
            newUser.UserName = userDto.Email;
            newUser.BarberShopId = Guid.NewGuid(); // Generate a unique BarberShopId

            var creationResult = await _userManager.CreateAsync(newUser, userDto.Password);
            if (!creationResult.Succeeded)
            {
                // Instead of returning null, you might throw an exception or return a result indicating failure
                throw new Exception("Failed to create user.");
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(newUser, "Administrator");
            if (!addToRoleResult.Succeeded)
            {
                // Handle failure to add to role
                throw new Exception("Failed to add user to Administrator role.");
            }

            // Return the DTO directly, without using Ok() or BadRequest()
            return new AuthResponseDto
            {
                UserId = newUser.Id,
                BarberShopId = newUser.BarberShopId.ToString()
                
            };
        }


        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(username);

            if (_user == null || _user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
    }
}