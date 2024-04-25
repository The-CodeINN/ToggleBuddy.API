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
        Task<ServiceResponse<FeatureResponseDto>> CreateFeatureAsync(FeatureRequestDto featureRequestDto, Guid projectId);

        Task<ServiceResponse<List<FeatureResponseDto>>> GetFeaturesByProjectIdAsync(Guid projectId);

        Task<ServiceResponse<FeatureResponseDto>> GetFeatureDetailsByProjectIdAsync(Guid projectId, Guid featureId);

        Task<ServiceResponse<UpdateFeatureResponseDto>> UpdateFeatureAsync(UpdateFeatureRequestDto featureRequestDto, Guid projectId, Guid featureId);

        Task<ServiceResponse<FeatureResponseDto>> DeleteFeatureAsync(Guid projectId, Guid featureId);
    }
}
