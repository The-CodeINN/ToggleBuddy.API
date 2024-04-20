using System;
using System.Threading.Tasks;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Respositories.Implementations;
using ToggleBuddy.API.Respositories.Interfaces;
using ToggleBuddy.API.Services.Interfaces;

namespace ToggleBuddy.API.Services.Interfaces
{
    public class FeatureServices(
                                IMapper mapper,
                                IFeatureRepository featureRepository,
                                IProjectRepository projectRespository,
                                IUser user
                                 ): IFeatureServices
    {
                            private readonly ApiResponse<object> _apiResponse = new ApiResponse<object>();
                            private IActionResult ApiResponse(object result, string message, ResponseStatus status)
                            {
                                _apiResponse.Result = result;
                                _apiResponse.Message = message;
                                _apiResponse.Status = status;

                                return Ok(_apiResponse);
                            }
      
       
       public async Task<FeatureRequestDto> CreateFeatureAsync(uid projectId, FeatureRequestDto featureRequestDto)
       {
           //check if the user is authenticated
            var userObject = await user.GetCurrentUserAsync(User);
            if (userObject == null)
            {
                return NotFound("User not found");
            }
            //check if the project belongs to the user
            var project = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
            if (project == null)
            {
                return BadRequest("The project does not belong to the user");
            }
            // Create a new instance of the model from the Dto
            var FeatureModel = new Feature
            {
                Name = FeatureRequestDto.Name,
                Description = FeatureRequestDto.Description,
                ExpirationDate = FeatureRequestDto.ExpirationDate,
                ProjectId = project.Id,
            };
            // Save the model using the repository class
            await featureRepository.CreateAsync(FeatureModel);
            return ApiResponse((mapper.Map<FeatureResponseDto>(FeatureModel)), "Feature created successfully", ResponseStatus.Success);
       }

      public async Task<FeatureResponseDto> ShowFeatureAsync(Project project, Guid id)
      {
          //check if the user is authenticated
            var userObject = await user.GetCurrentUserAsync(User);
            if (userObject == null)
            {
                 return NotFound("User not found");
            }

            //check if the project belongs to the user
            var projectUser = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
            if (projectUser == null)
            {
                return BadRequest("The project does not belong to the user");
            }
            var showFeature = await featureRepository.ShowAsync(projectUser, id);

            // check if the feature belongs to the project

            if(showFeature == null)
                return BadRequest("feature not found");
            return ApiResponse((mapper.Map<FeatureResponseDto>(showFeature)), "Feature retrieved by Id", ResponseStatus.Success);
      }

       public Task<FeatureResponseDto> UpdateFeatureAsync(Project project, Guid id)
       {
         //check if the user is authenticated
            var userObject = await user.GetCurrentUserAsync(User);
            if (userObject == null)
            {
                 return NotFound("User not found");
            }

            //check if the project belongs to the user
            var projectUser = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
            if (projectUser == null)
            {
               return BadRequest("The project does not belong to the user");
            }

            // Create a new instance of the model from the Dto

            var featureModel = new Feature 
            { 
                Name = updateFeatureRequestDto.Name,
                Description = updateFeatureRequestDto.Description,
                ExpirationDate=updateFeatureRequestDto.ExpirationDate,
            };

            var UpdateFeature = await featureRepository.UpdateAsync(projectUser, id, featureModel);
            // check if the feature belongs to the project

            if(UpdateFeature == null) 
            
                return NotFound();
            return ApiResponse((mapper.Map<FeatureResponseDto>(featureModel)), "Feature updated successfully", ResponseStatus.Success);
       }

       public Task<FeatureResponseDto> DeleteFeatureAsync(Project project, Guid id)
       {
           //check if the user is authenticated
            var userObject = await user.GetCurrentUserAsync(User);
            if (userObject == null)
            {   
                return NotFound("User not found");
            }

            //check if the project belongs to the user
            var project = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
            if (project == null)
            {
                return NotFound("Project not found");
            }
            var feature = await featureRepository.DeleteAsync(project, id);

            // check if the feature belongs to the project

            if(feature == null) 
            return BadRequest("feature not found");
            return ApiResponse((mapper.Map<FeatureResponseDto>(feature)), "Feature deleted successfully", ResponseStatus.Success);
       }
    }
}

