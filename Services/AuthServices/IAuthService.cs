using System.Security.Claims;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ServiceResponse<UserDto>> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ServiceResponse<UserDto>> GetCurrentUserInfoAsync(ClaimsPrincipal user);
    }

}

