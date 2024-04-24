using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Services.FeatureServices
{
    public interface IFeatureServices
    {
        Task<ApiResponse<FeatureResponseDto>> CreateFeatureAsync(FeatureRequestDto featureRequestDto, Guid projectId);

        Task<ApiResponse<List<FeatureResponseDto>>> GetFeaturesByProjectIdAsync(Guid projectId);

        Task<ApiResponse<FeatureResponseDto>> GetFeatureDetailsByProjectIdAsync(Guid projectId, Guid featureId);

        Task<ApiResponse<UpdateFeatureResponseDto>> UpdateFeatureAsync(UpdateFeatureRequestDto featureRequestDto, Guid projectId, Guid featureId);

        Task<ApiResponse<FeatureResponseDto>> DeleteFeatureAsync(Guid projectId, Guid featureId);
    }
}
