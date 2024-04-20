// using System;
// using System.Threading.Tasks;
// using AutoMapper;
// using Microsoft.AspNetCore.Http.HttpResults;
// using Microsoft.AspNetCore.Mvc;
// using ToggleBuddy.API.Helpers;
// using ToggleBuddy.API.Models.Domain;
// using ToggleBuddy.API.Models.DTOs.RequestDTOs;
// using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
// using ToggleBuddy.API.Respositories.Implementations;
// using ToggleBuddy.API.Respositories.Interfaces;
// using ToggleBuddy.API.Services.Interfaces;


// namespace ToggleBuddy.API.Services.Implementations
// {
//     public class FeatureServices(
//                                 IMapper mapper,
//                                 IFeatureRepository featureRepository,
//                                 IProjectRepository projectRespository,
//                                 IUser user
//                                  ): IFeatureServices
//     {
//                             private readonly ApiResponse<object> _apiResponse = new ApiResponse<object>();
//                             private IActionResult ApiResponse(object result, string message, ResponseStatus status)
//                             {
//                                 _apiResponse.Result = result;
//                                 _apiResponse.Message = message;
//                                 _apiResponse.Status = status;

//                                 return Ok(_apiResponse);
//                             }
      
       
//        public async Task<Feature> CreateFeatureAsync(Guid projectId, FeatureRequestDto featureRequestDto)
//        {
//            //check if the user is authenticated
//             var userObject = await user.GetCurrentUserAsync(User);
//             if (userObject == null)
//             {
//                 throw new UnauthorizedAccessException("User not found");
//             }
//             //check if the project belongs to the user
//             var project = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
//             if (project == null)
//             {
//                throw new ArgumentException("The project does not belong to the user", nameof(projectId));
//             }
//             // Create a new instance of the model from the Dto
//             var FeatureModel = new Feature
//             {
//                 Name = FeatureRequestDto.Name,
//                 Description = FeatureRequestDto.Description,
//                 ExpirationDate = FeatureRequestDto.ExpirationDate,
//                 ProjectId = project.Id,
//             };
//             // Save the model using the repository class
//             await featureRepository.CreateAsync(FeatureModel);
//             return ApiResponse((mapper.Map<FeatureResponseDto>(FeatureModel)), "Feature created successfully", ResponseStatus.Success);
//        }
//       public async Task<Feature> ShowFeatureAsync(Project project, Guid id)
//       {
//           //check if the user is authenticated
//             var userObject = await user.GetCurrentUserAsync(User);
//             if (userObject == null)
//             {
//                  throw new UnauthorizedAccessException("User not found");
//             }
//             //check if the project belongs to the user
//             var projectUser = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
//             if (projectUser == null)
//             {
//                throw new ArgumentException("The project does not belong to the user", nameof(projectId));
//             }
//             var showFeature = await featureRepository.ShowAsync(projectUser, id);
//             // check if the feature belongs to the project

//             if(showFeature == null)
//                 return BadRequest("feature not found");
//             return ApiResponse((mapper.Map<FeatureResponseDto>(showFeature)), "Feature retrieved by Id", ResponseStatus.Success);
//       }
//        public async Task<Feature> UpdateFeatureAsync(Project project, Guid id,UpdateFeatureRequestDto updateFeatureRequestDto)
//        {
//          //check if the user is authenticated
//             var userObject = await user.GetCurrentUserAsync(User);
//             if (userObject == null)
//             {
//                  throw new UnauthorizedAccessException("User not found");
//             }
//             //check if the project belongs to the user
//             var projectUser = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
//             if (projectUser == null)
//             {
//               throw new ArgumentException("The project does not belong to the user", nameof(projectId));
//             }
//             // Create a new instance of the model from the Dto
//             var featureModel = new Feature 
//             { 
//                 Name = updateFeatureRequestDto.Name,
//                 Description = updateFeatureRequestDto.Description,
//                 ExpirationDate=updateFeatureRequestDto.ExpirationDate,
//             };
//             var UpdateFeature = await featureRepository.UpdateAsync(projectUser, id, featureModel);
//             // check if the feature belongs to the project

//             if(UpdateFeature == null) 
            
//                 return NotFound();
//             return ApiResponse((mapper.Map<FeatureResponseDto>(featureModel)), "Feature updated successfully", ResponseStatus.Success);
//        }

//        public async Task<Feature> DeleteFeature(Project project, Guid id)
//        {
//            //check if the user is authenticated
//             var userObject = await user.GetCurrentUserAsync(User);
//             if (userObject == null)
//             {   
//                 throw new UnauthorizedAccessException("User not found");
//             }

//             //check if the project belongs to the user
//             var project = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
//             if (project == null)
//             {
//                throw new ArgumentException("The project does not belong to the user", nameof(projectId));
//             }
//             var feature = await featureRepository.DeleteAsync(project, id);

//             // check if the feature belongs to the project

//             if(feature == null) 
//             return BadRequest("feature not found");
//             return ApiResponse((mapper.Map<FeatureResponseDto>(feature)), "Feature deleted successfully", ResponseStatus.Success);
//        }
//     }
// }

// FeatureServices.cs
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Services.Interfaces;
using ToggleBuddy.API.Repositories.Interfaces;

namespace ToggleBuddy.API.Services.Implementations
{
    public class FeatureServices : IFeatureServices
    {
        private readonly IMapper _mapper;
        private readonly IFeatureRepository _featureRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUser _user;

        public FeatureServices(
            IMapper mapper,
            IFeatureRepository featureRepository,
            IProjectRepository projectRepository,
            IUser user)
        {
            _mapper = mapper;
            _featureRepository = featureRepository;
            _projectRepository = projectRepository;
            _user = user;
        }

        private IActionResult ApiResponse(object result, string message, ResponseStatus status)
        {
            return new OkObjectResult(new ApiResponse<object>
            {
                Result = result,
                Message = message,
                Status = status
            });
        }

        public async Task<Feature> CreateFeatureAsync(Project project, Guid id, FeatureRequestDto featureRequestDto)
        {
            var userObject = await _user.GetCurrentUserAsync(User);
            if (userObject == null)
            {
               return null;
            }

            var project = await _projectRepository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
            if (project == null)
            {
               return null;
            }

            var featureModel = new Feature
            {
                Name = featureRequestDto.Name,
                Description = featureRequestDto.Description,
                ExpirationDate = featureRequestDto.ExpirationDate,
                ProjectId = project.Id,
            };

            await _featureRepository.CreateAsync(featureModel);
            return ApiResponse(_mapper.Map<FeatureResponseDto>(featureModel), "Feature created successfully", ResponseStatus.Success);
        }

        public async Task<Feature> ShowFeatureAsync(Project project, Guid id)
        {
            var userObject = await _user.GetCurrentUserAsync(User);
            if (userObject == null)
            {
               return null;
            }

            var projectUser = await _projectRepository.GetProjectByIdForCurrentUserAsync(project.Id, userObject.Id);
            if (projectUser == null)
            {
                return null;
            }

            var showFeature = await _featureRepository.ShowAsync(projectUser, id);

            if (showFeature == null)
                return null;

            return ApiResponse(_mapper.Map<FeatureResponseDto>(showFeature), "Feature retrieved by Id", ResponseStatus.Success);
        }

        public async Task<Feature> UpdateFeatureAsync(Project project, Guid id, UpdateFeatureRequestDto updateFeatureRequestDto)
        {
            var userObject = await _user.GetCurrentUserAsync(User);
            if (userObject == null)
            {
                return null;
            }

            var projectUser = await _projectRepository.GetProjectByIdForCurrentUserAsync(project.Id, userObject.Id);
            if (projectUser == null)
            {
                return null;
            }

            var featureModel = new Feature
            {
                Name = updateFeatureRequestDto.Name,
                Description = updateFeatureRequestDto.Description,
                ExpirationDate = updateFeatureRequestDto.ExpirationDate,
            };

            var updatedFeature = await _featureRepository.UpdateAsync(projectUser, id, featureModel);

            if (updatedFeature == null)
               return null;

            return ApiResponse(_mapper.Map<FeatureResponseDto>(featureModel), "Feature updated successfully", ResponseStatus.Success);
        }

        public async Task<Feature> DeleteFeature(Project project, Guid id)
        {
            var userObject = await _user.GetCurrentUserAsync(User);
            if (userObject == null)
            {
               return null;;
            }

            var projectUser = await _projectRepository.GetProjectByIdForCurrentUserAsync(project.Id, userObject.Id);
            if (projectUser == null)
            {
              return null;
            }

            var feature = await _featureRepository.DeleteAsync(projectUser, id);

            if (feature == null)
                return null;

            return ApiResponse(_mapper.Map<FeatureResponseDto>(feature), "Feature deleted successfully", ResponseStatus.Success);
        }
    }
}
