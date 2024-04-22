using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Services.Interfaces
{
    public interface IFeatureServices
    {
        Task<ApiResponse<FeatureResponseDto>> CreateFeatureAsync(Project project, ClaimsPrincipal userId, FeatureRequestDto featureRequestDto);

        Task<ApiResponse<FeatureResponseDto>> ShowFeatureAsync(Project project, Guid id,ClaimsPrincipal userId);

        Task<ApiResponse<FeatureResponseDto>> UpdateFeatureAsync(Project project, Guid id, UpdateFeatureRequestDto updateFeatureRequestDto,ClaimsPrincipal userId);

        Task<ApiResponse<FeatureResponseDto>> DeleteFeature(Project project, Guid id,ClaimsPrincipal userId);
    }
}
