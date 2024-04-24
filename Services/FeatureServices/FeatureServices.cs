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

        public async Task<ApiResponse<FeatureResponseDto>> CreateFeatureAsync(FeatureRequestDto featureRequestDto, Guid projectId)
        {
            var featureModel = _mapper.Map<Feature>(featureRequestDto);
            featureModel.ProjectId = projectId;

            var createdFeature = await _featureRepository.CreateAsync(featureModel, projectId);

            var featureResponseDto = _mapper.Map<FeatureResponseDto>(createdFeature);

            return new ApiResponse<FeatureResponseDto> { Result = featureResponseDto, Message = "Feature created successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<FeatureResponseDto>> DeleteFeatureAsync(Guid projectId, Guid featureId)
        {
            var feature = await _featureRepository.DeleteAsync(projectId, featureId);

            if (feature == null)
                return new ApiResponse<FeatureResponseDto> { Message = "Feature not found", Status = ResponseStatus.NotFound };

            return new ApiResponse<FeatureResponseDto> { Result = _mapper.Map<FeatureResponseDto>(feature), Message = "Feature deleted successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<FeatureResponseDto>> GetFeatureDetailsByProjectIdAsync(Guid projectId, Guid featureId)
        {
            var feature = await _featureRepository.ShowAsync(projectId, featureId);

            if (feature == null)
                return new ApiResponse<FeatureResponseDto> { Message = "Feature not found", Status = ResponseStatus.NotFound };

            return new ApiResponse<FeatureResponseDto> { Result = _mapper.Map<FeatureResponseDto>(feature), Message = "Feature retrieved successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<List<FeatureResponseDto>>> GetFeaturesByProjectIdAsync(Guid projectId)
        {
            var features = await _featureRepository.GetAllAsync(projectId);

            if (features == null)
                return new ApiResponse<List<FeatureResponseDto>> { Message = "Features not found", Status = ResponseStatus.NotFound };

            var projectResponseDtos = _mapper.Map<List<FeatureResponseDto>>(features);

            return new ApiResponse<List<FeatureResponseDto>> { Result = projectResponseDtos, Message = "Features retrieved successfully", Status = ResponseStatus.Success };

            // return  list of features
        }

        public async Task<ApiResponse<UpdateFeatureResponseDto>> UpdateFeatureAsync(UpdateFeatureRequestDto featureRequestDto, Guid projectId, Guid featureId)
        {
            var featureModel = _mapper.Map<Feature>(featureRequestDto);

            var updatedFeature = await _featureRepository.UpdateAsync(featureModel, projectId, featureId);

            if (updatedFeature == null)
                return new ApiResponse<UpdateFeatureResponseDto> { Message = "Feature not found", Status = ResponseStatus.NotFound };

            return new ApiResponse<UpdateFeatureResponseDto> { Result = _mapper.Map<UpdateFeatureResponseDto>(updatedFeature), Message = "Feature updated successfully", Status = ResponseStatus.Success };
        }



        // public async Task<ApiResponse<FeatureResponseDto>> CreateFeatureAsync(Project project, ClaimsPrincipal userId, FeatureRequestDto featureRequestDto)
        // {
        //     var userObject = await _user.GetCurrentUserAsync(userId);
        //     if (userObject == null) return new ApiResponse<FeatureResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };

        //     var projects = await _projectRepository.GetProjectByIdAsync(project.Id, userObject.Id);
        //     if (projects == null)

        //         return new ApiResponse<FeatureResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };


        //     var featureModel = new Feature
        //     {
        //         Name = featureRequestDto.Name,
        //         Description = featureRequestDto.Description,
        //         ExpirationDate = featureRequestDto.ExpirationDate,
        //         ProjectId = project.Id
        //     };

        //     await _featureRepository.CreateAsync(featureModel);
        //     return new ApiResponse<FeatureResponseDto> { Result = _mapper.Map<FeatureResponseDto>(featureModel), Message = "Feature created successfully", Status = ResponseStatus.Success };


        // }


        // public async Task<ApiResponse<FeatureResponseDto>> DeleteFeature(Project project, Guid id, ClaimsPrincipal userId)
        // {
        //     var userObject = await _user.GetCurrentUserAsync(userId);
        //     if (userObject == null) return new ApiResponse<FeatureResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };

        //     var projects = await _projectRepository.GetProjectByIdAsync(project.Id, userObject.Id);
        //     if (projects == null)

        //         return new ApiResponse<FeatureResponseDto> { Message = "Project", Status = ResponseStatus.NotFound };
        //     var feature = await _featureRepository.DeleteAsync(project, id);

        //     if (feature == null)
        //         return new ApiResponse<FeatureResponseDto> { Message = "Feature", Status = ResponseStatus.NotFound };

        //     return new ApiResponse<FeatureResponseDto> { Result = _mapper.Map<FeatureResponseDto>(feature), Message = "Feature deleted successfully", Status = ResponseStatus.Success };
        // }

        // public async Task<ApiResponse<FeatureResponseDto>> ShowFeatureAsync(Project project, Guid id, ClaimsPrincipal userId)
        // {
        //     var userObject = await _user.GetCurrentUserAsync(userId);
        //     if (userObject == null) return new ApiResponse<FeatureResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };

        //     var projects = await _projectRepository.GetProjectByIdAsync(project.Id, userObject.Id);
        //     if (projects == null)

        //         return new ApiResponse<FeatureResponseDto> { Message = "Project", Status = ResponseStatus.NotFound };
        //     var feature = await _featureRepository.ShowAsync(project, id);

        //     if (feature == null)
        //         return new ApiResponse<FeatureResponseDto> { Message = "Feature", Status = ResponseStatus.NotFound };

        //     return new ApiResponse<FeatureResponseDto> { Result = _mapper.Map<FeatureResponseDto>(feature), Message = "Feature retrieved by Id", Status = ResponseStatus.Success };

        // }

        // public async Task<ApiResponse<FeatureResponseDto>> UpdateFeatureAsync(Project project, Guid id, UpdateFeatureRequestDto updateFeatureRequestDto, ClaimsPrincipal userId)
        // {
        //     var userObject = await _user.GetCurrentUserAsync(userId);
        //     if (userObject == null)

        //         return new ApiResponse<FeatureResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };


        //     var projectUser = await _projectRepository.GetProjectByIdAsync(project.Id, userObject.Id);
        //     if (projectUser == null)
        //         return new ApiResponse<FeatureResponseDto> { Message = "Project", Status = ResponseStatus.NotFound };

        //     var featureModel = new Feature
        //     {
        //         Name = updateFeatureRequestDto.Name,
        //         Description = updateFeatureRequestDto.Description,
        //         ExpirationDate = updateFeatureRequestDto.ExpirationDate,
        //     };

        //     var updatedFeature = await _featureRepository.UpdateAsync(projectUser, id, featureModel);

        //     if (updatedFeature == null)
        //         return new ApiResponse<FeatureResponseDto> { Message = "feature", Status = ResponseStatus.NotFound };
        //     var featureResponseDto = _mapper.Map<FeatureResponseDto>(featureModel);

        //     return new ApiResponse<FeatureResponseDto> { Result = featureResponseDto, Message = "Project updated successfully", Status = ResponseStatus.Success };

        // }
    }
}
