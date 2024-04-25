using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Repositories.Interfaces;

namespace ToggleBuddy.API.Services.AuthServices
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenRepository tokenRepository, IUser user, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
            _user = user;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<UserDto>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var user = new User
            {
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);


            if (!result.Succeeded)
            {
                return new ServiceResponse<UserDto> { Message = result.Errors.FirstOrDefault()?.Description ?? "User registration failed", Status = ResponseStatus.Error };
            }

            var registerResponse = _mapper.Map<UserDto>(user);

            return new ServiceResponse<UserDto> { Result = registerResponse, Message = "User registered successfully", Status = ResponseStatus.Success };
        }

        public async Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                return new ServiceResponse<LoginResponseDto> { Message = "User not found", Status = ResponseStatus.Error };
            }

            // Modify lockoutOnFailure to true when ready to implement account lockout
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return new ServiceResponse<LoginResponseDto> { Message = "Invalid login attempt", Status = ResponseStatus.Error };
            }

            var token = _tokenRepository.CreateJWTToken(user);

            var loginResponse = new LoginResponseDto
            {
                Token = token,
                User = _mapper.Map<UserDto>(user)
            };

            return new ServiceResponse<LoginResponseDto> { Result = loginResponse, Message = "User logged in successfully", Status = ResponseStatus.Success };

        }

        public async Task<ServiceResponse<UserDto>> GetCurrentUserInfoAsync(ClaimsPrincipal userId)
        {
            var user = await _user.GetCurrentUserAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<UserDto> { Message = "User not found", Status = ResponseStatus.NotFound };
            }

            var userResponse = _mapper.Map<UserDto>(user);

            return new ServiceResponse<UserDto> { Result = userResponse, Message = "User retrieved successfully", Status = ResponseStatus.Success };
        }
    }
}