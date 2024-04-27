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

        public async Task<ServiceResponse<FeatureEnvironmentResponseDto>> CreateFeatureEnvironmentAsync(FeatureEnvironmentRequestDto featureEnvironmentRequestDto, Guid featureId)
        {  
            var featureEnvironmentModel = _mapper.Map<FeatureEnvironment>(featureEnvironmentRequestDto);
            featureEnvironmentModel.FeatureId = featureId;
 
            var newEnvironment = await _featureEnvironmentRepository.CreateFeatureEnvironmentAsync(featureEnvironmentModel, featureId);
            var featureEnvironmentResponseDto = _mapper.Map<FeatureEnvironmentResponseDto>(newEnvironment);
            return new ServiceResponse<FeatureEnvironmentResponseDto> { Result = featureEnvironmentResponseDto, Message = "Feature environment created successfully", Status = ResponseStatus.Success };
        }

        public async Task<ServiceResponse<List<FeatureEnvironmentResponseDto>>> ShowAllFeatureEnvironmentsAsync(Guid featureId)
        {
            // ... implementation ...
            var environments = await _featureEnvironmentRepository.GetAllFeatureEnvironmentsAsync(featureId);
            if (environments == null)
                return new ServiceResponse<List<FeatureEnvironmentResponseDto>> { Message = "Feature environments not found", Status = ResponseStatus.NotFound };

            var featureResponseDtos = _mapper.Map<List<FeatureEnvironmentResponseDto>>(environments);
            return new ServiceResponse<List<FeatureEnvironmentResponseDto>> { Result = featureResponseDtos, Message = "Feature environments retrieved successfully", Status = ResponseStatus.Success };
            
        }

        public async Task<ServiceResponse<FeatureEnvironmentResponseDto>> GetFeatureEnvironmentByIdAsync(Guid featureId, Guid featureEnvironmentId)
        {
            // ... implementation ...
            var featureEnvironment = await _featureEnvironmentRepository.GetFeatureEnvironmentByIdForCurrentFeatureAsync(featureEnvironmentId, featureId);
            if (featureEnvironment == null)
                return new ServiceResponse<FeatureEnvironmentResponseDto> { Message = "Feature environment not found", Status = ResponseStatus.NotFound };

            var featureEnvironmentResponseDto = _mapper.Map<FeatureEnvironmentResponseDto>(featureEnvironment);
            return new ServiceResponse<FeatureEnvironmentResponseDto> { Result = featureEnvironmentResponseDto, Message = "Feature environment retrieved successfully", Status = ResponseStatus.Success };
        }
        
        public async Task<ServiceResponse<FeatureEnvironmentResponseDto>> UpdateFeatureEnvironmentAsync(Guid featureId, Guid featureEnvironmentId, FeatureEnvironmentRequestDto featureEnvironmentRequestDto)
        {
            // ... implementation ...
            var featureEnvironment = await _featureEnvironmentRepository.GetFeatureEnvironmentByIdForCurrentFeatureAsync(featureEnvironmentId, featureId);
            if (featureEnvironment == null)
                return new ServiceResponse<FeatureEnvironmentResponseDto> { Message = "Feature environment not found", Status = ResponseStatus.NotFound };

            var featureEnvironmentModel = _mapper.Map<FeatureEnvironment>(featureEnvironmentRequestDto);
            featureEnvironmentModel.FeatureId = featureId;
            featureEnvironmentModel.Id = featureEnvironmentId;
            return new ServiceResponse<FeatureEnvironmentResponseDto> { Result = _mapper.Map<FeatureEnvironmentResponseDto>(featureEnvironmentModel), Message = "Feature environment updated successfully", Status = ResponseStatus.Success };
        }

        
        public async Task<ServiceResponse<FeatureEnvironmentResponseDto>> DeleteFeatureEnvironmentAsync(Guid featureId, Guid featureEnvironmentId)
        {
            // ... implementation ...
            var featureEnvironment = await _featureEnvironmentRepository.GetFeatureEnvironmentByIdForCurrentFeatureAsync(featureEnvironmentId, featureId);
            if (featureEnvironment == null)
                return new ServiceResponse<FeatureEnvironmentResponseDto> { Message = "Feature environment not found", Status = ResponseStatus.NotFound };

            await _featureEnvironmentRepository.DeleteFeatureEnvironmentAsync(featureId, featureEnvironmentId);
            throw new NotImplementedException();
        }
    }
}