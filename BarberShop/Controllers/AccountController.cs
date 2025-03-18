using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BarberShop.Contract;
using BarberShop.Models.User;
using Microsoft.AspNetCore.Http;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;
       

        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }
        // POST: api/Account/ValidateUser
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ValidateUser([FromBody] ApiUserDto apiUserDto)
        {
            _logger.LogInformation($"Registration Attempt for{apiUserDto.Email}");
            var errors = await _authManager.ValidateUser(apiUserDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();

        }
        // POST: api/Account/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation($"Login Attempt for {loginDto.Email}");

            var authResponse = await _authManager.Login(loginDto);

            if (authResponse == null)
            {
                _logger.LogWarning($"Unauthorized login attempt for {loginDto.Email}");
                return Unauthorized();
            }

            _logger.LogInformation($"User {loginDto.Email} logged in successfully.");
            return Ok(authResponse);
        }





        [HttpPost]
        [Route("register-admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateAdminUser([FromBody] ApiUserDto apiUserDto)
        {
            var errors = await _authManager.CreateAdminUser(apiUserDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }
        // POST: api/Account/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
        [HttpPost("create-admin-account")]
        public async Task<ActionResult<AuthResponseDto>> CreateAdminAccount([FromBody] ApiUserDto userDto)
        {
            try
            {
                var authResponse = await _authManager.CreateAdminAccountAsync(userDto);
                _logger.LogInformation($"Admin account created successfully for {userDto.Email}");
                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create admin account for {Email}", userDto.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the admin account.");
            }
        }
    }
}

