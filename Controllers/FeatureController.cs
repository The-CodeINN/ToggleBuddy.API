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
        IProjectRespository projectRespository,
        IUser user       
        ) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        [ValidateModel]

        public async Task<IActionResult> CreateAsync(
            [FromRoute] Guid projectId,
            [FromBody] FeatureRequestDto FeatureRequestDto
            )
        {
            //check if the user is authenticated
            var userObject =  await user.GetCurrentUserAsync(User);
            if( userObject == null ) 
            {
                return NotFound();
            }
            // check if the project exists
           // var projectId = await projectRespository.GetProjectsAsync()
            //check if the project belongs to the user

            var project = await projectRespository.GetProjectByIdForCurrentUserAsync(projectId,  userObject.Id);
            if( project == null ) 
            {
                return NotFound();
            }
            // Create a new instance of the model from the Dto
            var FeatureModel = new Feature
            {
                Name = FeatureRequestDto.Name,
                Description = FeatureRequestDto.Description,
                ExpirationDate = FeatureRequestDto.ExpirationDate,
            };
            // Save the model using the repository class
            await featureRepository.CreateAsync(FeatureModel);
            return Ok((mapper.Map<FeatureResponseDto>(FeatureModel)));


        }

    }
}
