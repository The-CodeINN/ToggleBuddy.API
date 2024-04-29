using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Respositories.Implementations;
using ToggleBuddy.API.Respositories.Interfaces;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenRepository tokenRepository;
        private readonly IUser user;
        private readonly IMapper mapper;
        private readonly ApiResponse<object> apiResponse = new ApiResponse<object>();

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenRepository tokenRepository,
            IUser user,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenRepository = tokenRepository;
            this.user = user;
            this.mapper = mapper;

        }
        [HttpPost]
        [ValidateModel]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var user = new User
            {
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Email
            };

            var result = await userManager.CreateAsync(user, registerRequestDto.Password);

            if (!result.Succeeded)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = result.Errors.FirstOrDefault()?.Description ?? "User registration failed";
                return BadRequest(apiResponse);
            }

            apiResponse.Status = ResponseStatus.Success;
            apiResponse.Message = "User registered successfully";
            apiResponse.Result = mapper.Map<UserDto>(user);

            return Ok(apiResponse);
        }

        [HttpPost]
        [ValidateModel]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "Invalid email or password";
                return BadRequest(apiResponse);
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "Invalid email or password";
                return BadRequest(apiResponse);
            }

            var token = tokenRepository.CreateJWTToken(user);

            apiResponse.Status = ResponseStatus.Success;
            apiResponse.Message = "User logged in successfully";
            apiResponse.Result = new { Token = token, User = mapper.Map<UserDto>(user) };

            return Ok(apiResponse);
        }

        // GET: api/Auth/me
        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var currentUser = await user.GetCurrentUserAsync(User);
            if (currentUser == null)
            {
                apiResponse.Message = "User not found";
                apiResponse.Result = null;
                apiResponse.Status = ResponseStatus.Error;
                return NotFound(apiResponse);
            }

            var userResponseDto = mapper.Map<UserDto>(currentUser);

            apiResponse.Result = userResponseDto;
            apiResponse.Message = "User found";

            return Ok(apiResponse);
        }
    }
}
