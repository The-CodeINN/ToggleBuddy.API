using System.Security.Claims;
using AutoMapper;
using System;   
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Services.FeatureServices;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Repositories.Interfaces;


namespace ToggleBuddy.API.Services.FeatureEnvironmentServices
{
    public class FeatureEnvironmentServices : IFeatureEnvironmentServices
    {
        private readonly IMapper _mapper;
        private readonly IFeatureEnvironmentRepository _featureEnvironmentRepository;

        public FeatureEnvironmentServices(
                IMapper mapper,
                IFeatureEnvironmentRepository featureEnvironmentRepository
               )
        {
            _mapper = mapper;
            _featureEnvironmentRepository = featureEnvironmentRepository;
        }

        public async Task<ApiResponse<FeatureEnvironmentResponseDto>> CreateFeatureEnvironmentAsync(Guid featureId, FeatureEnvironmentRequestDto featureEnvironmentRequestDto)
        {  
            var featureEnvironmentModel = _mapper.Map<FeatureEnvironment>(featureEnvironmentRequestDto);
            featureEnvironmentModel.FeatureId = featureId.ToString();
 
            var newEnvironment = await _featureEnvironmentRepository.CreateFeatureEnvironmentAsync(featureId, featureEnvironmentModel);
            var featureEnvironmentResponseDto = _mapper.Map<FeatureEnvironmentResponseDto>(newEnvironment);
            return new ApiResponse<FeatureEnvironmentResponseDto> { Result = featureEnvironmentResponseDto, Message = "Feature environment created successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<List<FeatureEnvironmentResponseDto>>> ShowAllFeatureEnvironmentsAsync(Guid featureId)
        {
            // ... implementation ...
            var environments = await _featureEnvironmentRepository.GetAllFeatureEnvironmentsAsync(featureId);
            if (environments == null)
                return new ApiResponse<List<FeatureEnvironmentResponseDto>> { Message = "Feature environments not found", Status = ResponseStatus.NotFound };

            var featureResponseDtos = _mapper.Map<List<FeatureEnvironmentResponseDto>>(environments);
            return new ApiResponse<List<FeatureEnvironmentResponseDto>> { Result = featureResponseDtos, Message = "Feature environments retrieved successfully", Status = ResponseStatus.Success };
            
        }

        public async Task<ApiResponse<FeatureEnvironmentResponseDto>> GetFeatureEnvironmentByIdAsync(Guid featureId, Guid featureEnvironmentId)
        {
            // ... implementation ...

            throw new NotImplementedException();
        }
        
        public async Task<ApiResponse<FeatureEnvironmentResponseDto>> UpdateFeatureEnvironmentAsync(Guid featureId, Guid id, FeatureEnvironmentRequestDto featureEnvironmentRequestDto)
        {
            // ... implementation ...
            throw new NotImplementedException();
        }

        
        public async Task<ApiResponse<FeatureEnvironmentResponseDto>> DeleteFeatureEnvironment(Guid featureId, Guid featureEnvironmentId)
        {
            // ... implementation ...
            throw new NotImplementedException();
        }
    }
}