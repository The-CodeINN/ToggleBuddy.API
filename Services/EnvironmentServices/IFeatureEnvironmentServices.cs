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
        Task<ServiceResponse<FeatureEnvironmentResponseDto>> CreateFeatureEnvironmentAsync(Guid featureId, FeatureEnvironmentRequestDto featureEnvironmentRequestDto);

        Task<ServiceResponse<List<FeatureEnvironmentResponseDto>>> ShowAllFeatureEnvironmentsAsync(Guid featureId);

        Task<ServiceResponse<FeatureEnvironmentResponseDto>> GetFeatureEnvironmentByIdAsync(Guid featureId, Guid featureEnvironmentId);

        Task<ServiceResponse<FeatureEnvironmentResponseDto>> UpdateFeatureEnvironmentAsync(Guid featureId, Guid featureEnvironmentId, FeatureEnvironmentRequestDto FeatureEnvironmentRequestDto);

        Task<ServiceResponse<FeatureEnvironmentResponseDto>> DeleteFeatureEnvironment(Guid featureId, Guid featureEnvironmentId);
    }
}
