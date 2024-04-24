using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Services.FeatureEnvironmentServices
{
    public interface IFeatureEnvironmentServices
    {
        Task<ApiResponse<FeatureEnvironmentResponseDto>> CreateFeatureEnvironmentAsync(Project project, Feature feature, FeatureEnvironmentRequestDto featureEnvironmentRequestDto);

        Task<ApiResponse<FeatureEnvironmentResponseDto>> ShowFeatureEnvironmentAsync(Feature feature, Guid id, ClaimsPrincipal userId);

        // Task<ApiResponse<FeatureEnvironmentResponseDto>> UpdateFeatureEnvironmentAsync(Feature feature, Guid id, UpdateFeatureEnvironmentRequestDto updateFeatureEnvironmentRequestDto, ClaimsPrincipal userId);

        Task<ApiResponse<FeatureEnvironmentResponseDto>> DeleteFeatureEnvironment(Feature feature, Guid id, ClaimsPrincipal userId);
    }
}
