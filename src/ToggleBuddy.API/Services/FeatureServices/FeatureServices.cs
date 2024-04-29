using System;
using AutoMapper;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using System.Security.Claims;
using ToggleBuddy.API.Repositories.Interfaces;

namespace ToggleBuddy.API.Services.FeatureServices
{
    public class FeatureServices : IFeatureServices
    {
        private readonly IMapper _mapper;
        private readonly IFeatureRepository _featureRepository;

        public FeatureServices(
                IMapper mapper,
                IFeatureRepository featureRepository
               )
        {
            _mapper = mapper;
            _featureRepository = featureRepository;
        }

        public async Task<ServiceResponse<FeatureResponseDto>> CreateFeatureAsync(FeatureRequestDto featureRequestDto, Guid projectId)
        {
            var featureModel = _mapper.Map<Feature>(featureRequestDto);
            featureModel.ProjectId = projectId;

            var createdFeature = await _featureRepository.CreateAsync(featureModel, projectId);

            var featureResponseDto = _mapper.Map<FeatureResponseDto>(createdFeature);

            return new ServiceResponse<FeatureResponseDto> { Result = featureResponseDto, Message = "Feature created successfully", Status = ResponseStatus.Success };
        }

        public async Task<ServiceResponse<FeatureResponseDto>> DeleteFeatureAsync(Guid projectId, Guid featureId)
        {
            var feature = await _featureRepository.DeleteAsync(projectId, featureId);

            if (feature == null)
                return new ServiceResponse<FeatureResponseDto> { Message = "Feature not found", Status = ResponseStatus.NotFound };

            return new ServiceResponse<FeatureResponseDto> { Result = _mapper.Map<FeatureResponseDto>(feature), Message = "Feature deleted successfully", Status = ResponseStatus.Success };
        }

        public async Task<ServiceResponse<FeatureResponseDto>> GetFeatureDetailsByProjectIdAsync(Guid projectId, Guid featureId)
        {
            var feature = await _featureRepository.ShowAsync(projectId, featureId);

            if (feature == null)
                return new ServiceResponse<FeatureResponseDto> { Message = "Feature not found", Status = ResponseStatus.NotFound };

            return new ServiceResponse<FeatureResponseDto> { Result = _mapper.Map<FeatureResponseDto>(feature), Message = "Feature retrieved successfully", Status = ResponseStatus.Success };
        }

        public async Task<ServiceResponse<List<FeatureResponseDto>>> GetFeaturesByProjectIdAsync(Guid projectId)
        {
            var features = await _featureRepository.GetAllAsync(projectId);

            if (features == null)
                return new ServiceResponse<List<FeatureResponseDto>> { Message = "Features not found", Status = ResponseStatus.NotFound };

            var projectResponseDtos = _mapper.Map<List<FeatureResponseDto>>(features);

            return new ServiceResponse<List<FeatureResponseDto>> { Result = projectResponseDtos, Message = "Features retrieved successfully", Status = ResponseStatus.Success };

            // return  list of features
        }

        public async Task<ServiceResponse<UpdateFeatureResponseDto>> UpdateFeatureAsync(UpdateFeatureRequestDto featureRequestDto, Guid projectId, Guid featureId)
        {
            var featureModel = _mapper.Map<Feature>(featureRequestDto);

            var updatedFeature = await _featureRepository.UpdateAsync(featureModel, projectId, featureId);

            if (updatedFeature == null)
                return new ServiceResponse<UpdateFeatureResponseDto> { Message = "Feature not found", Status = ResponseStatus.NotFound };

            return new ServiceResponse<UpdateFeatureResponseDto> { Result = _mapper.Map<UpdateFeatureResponseDto>(updatedFeature), Message = "Feature updated successfully", Status = ResponseStatus.Success };
        }

    }
}