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
        Task<ApiResponse<FeatureEnvironmentResponseDto>> CreateFeatureEnvironmentAsync(Guid featureId, FeatureEnvironmentRequestDto featureEnvironmentRequestDto);

        Task<ApiResponse<List<FeatureEnvironmentResponseDto>>> ShowAllFeatureEnvironmentsAsync(Guid featureId);

        Task<ApiResponse<FeatureEnvironmentResponseDto>> GetFeatureEnvironmentByIdAsync(Guid featureId, Guid featureEnvironmentId);

        Task<ApiResponse<FeatureEnvironmentResponseDto>> UpdateFeatureEnvironmentAsync(Guid featureId, Guid featureEnvironmentId, FeatureEnvironmentRequestDto FeatureEnvironmentRequestDto);

        Task<ApiResponse<FeatureEnvironmentResponseDto>> DeleteFeatureEnvironment(Guid featureId, Guid featureEnvironmentId);
    }
}
