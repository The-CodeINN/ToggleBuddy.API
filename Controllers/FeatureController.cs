using AutoMapper;
using AutoMapper.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Respositories.Implementations;
using ToggleBuddy.API.Respositories.Interfaces;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/{projectId:Guid}/Feature")]
    [ApiController]
    public class FeatureController(
        IMapper mapper,
        IFeatureRepository featureRepository,
        IProjectRepository projectRespository,
        IUser user
        //ApiResponse<string> _apiResponse

        ) : ControllerBase
    {
        private readonly ApiResponse<object> _apiResponse = new ApiResponse<object>();
        private IActionResult ApiResponse(object result, string message, ResponseStatus status)
        {
            _apiResponse.Result = result;
            _apiResponse.Message = message;
            _apiResponse.Status = status;

            return Ok(_apiResponse);
        }
        [HttpPost]
        [Authorize]
        [ValidateModel]

        public async Task<IActionResult> Create(
            [FromRoute] Guid projectId,
            [FromBody] FeatureRequestDto FeatureRequestDto
            )


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

        [HttpGet]
        [Authorize]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Show([FromRoute] Guid id, [FromRoute] Guid projectId )
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

        [HttpPut]
        [Authorize]
        [ValidateModel]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid projectId, [FromRoute] Guid id, [FromBody] UpdateFeatureRequestDto updateFeatureRequestDto)
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

        [HttpDelete]
        [Authorize]
        [ValidateModel]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid projectId, [FromRoute] Guid id )
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
