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

        ) : ControllerBase
    {
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
                return NotFound();
            }
            //check if the project belongs to the user
            var project = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
            if (project == null)
            {
                return NotFound();
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
            return Ok((mapper.Map<FeatureResponseDto>(FeatureModel)));
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
                return NotFound();
            }

            //check if the project belongs to the user
            var projectUser = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
            if (projectUser == null)
            {
                return NotFound();
            }
            var showFeature = await featureRepository.ShowAsync(projectUser, id);

            // check if the feature belongs to the project

            if(showFeature == null)
                return NotFound();
            return Ok((mapper.Map<FeatureResponseDto>(showFeature)));
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
                return NotFound();
            }

            //check if the project belongs to the user
            var projectUser = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
            if (projectUser == null)
            {
                return NotFound();
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
            return Ok((mapper.Map<UpdateFeatureResponseDto>(featureModel)));
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
                return NotFound();
            }

            //check if the project belongs to the user
            var project = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId, userObject.Id);
            if (project == null)
            {
                return NotFound();
            }
            var feature = await featureRepository.DeleteAsync(project, id);

            // check if the feature belongs to the project

            if(feature == null)
            {   
                return NotFound();
            }

            return Ok("delected successfully");
        } 
    }
}
