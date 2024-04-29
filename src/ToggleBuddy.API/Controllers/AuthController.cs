using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Services.AuthServices;

namespace ToggleBuddy.API.Controllers
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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var response = await _authService.RegisterAsync(registerRequestDto);
            return Utilities.HandleApiResponse(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var response = await _authService.LoginAsync(loginRequestDto);
            return Utilities.HandleApiResponse(response);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var response = await _authService.GetCurrentUserInfoAsync(User);
            return Utilities.HandleApiResponse(response);
        }
    }
}