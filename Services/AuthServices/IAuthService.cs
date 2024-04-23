using System.Security.Claims;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ApiResponse<UserDto>> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ApiResponse<UserDto>> GetCurrentUserInfoAsync(ClaimsPrincipal user);
    }

}

