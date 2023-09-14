using Booking.Models.DTOModels;
using Booking.Services.AuthService.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO data)
        {
            var user = await _authService.Authenticate(data);
            if (user == null)
                return Unauthorized();
            // User found, generate and return JWT token
            var token = _authService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(SignUpDTO registrationData)
        {
            try
            {
                // You should validate the registrationData here, e.g., check for required fields, unique email, etc.

                // Call your authentication service to register the user
                var userId = await _authService.Register(registrationData);

                // Optionally, you can generate a JWT token for the registered user and return it here

                return Ok(new { UserId = userId, Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Registration failed", Error = ex.Message });
            }
        }

    }
}
