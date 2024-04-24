using System.Security.Claims;
using AutoMapper;
using System;   
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Repositories.Interfaces;


namespace ToggleBuddy.API.Services.FeatureEnvironmentServices
{
    public class FeatureEnvironmentServices : IFeatureEnvironmentServices
    {
        private readonly IMapper _mapper;
        private readonly IFeatureEnvironmentRepository _featureEnvironmentRepository;
        private readonly IFeatureRepository _featureRepository;

        public IFeatureEnvironmentServices(
                IMapper mapper,
                IFeatureRepository featureRepository,
                IFeatureEnvironmentRepository featureEnvironmentRepository
               )
        {
            _mapper = mapper;
            _featureRepository = featureRepository;
            _featureEnvironmentRepository = featureEnvironmentRepository;
        }

        public async Task<ApiResponse<FeatureEnvironmentResponseDto>> CreateFeatureEnvironmentAsync(Project project, Feature feature, FeatureEnvironmentRequestDto featureEnvironmentRequestDto)
        {
            var features = await _featureRepository.ShowAsync(project, feature.Id);
            if (features == null)
                return new ApiResponse<FeatureEnvironmentResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };
            
            var featureEnvironmentModel = new FeatureEnvironment
            {
                Name = featureEnvironmentRequestDto.Name,
                Description = featureEnvironmentRequestDto.Description,
                IsEnabled = featureEnvironmentRequestDto.IsEnabled,
                FeatureId = feature.Id.ToString()

            };
            await _featureEnvironmentRepository.CreateFeatureEnvironmentAsync(featureEnvironmentModel);
            return new ApiResponse<FeatureEnvironmentResponseDto> { Result = _mapper.Map<FeatureEnvironmentResponseDto>(featureEnvironmentModel), Message = "Feature created successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<FeatureEnvironmentResponseDto>> ShowFeatureEnvironmentAsync(Feature feature, Guid id, ClaimsPrincipal user)
    {
        // ... implementation ...
    }

    public async Task<ApiResponse<FeatureEnvironmentResponseDto>> DeleteFeatureEnvironment(Feature feature, Guid id, ClaimsPrincipal user)
    {
        // ... implementation ...
    }
    }
}