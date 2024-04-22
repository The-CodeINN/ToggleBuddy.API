using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Services.Interfaces;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Repositories.Interfaces;
using ToggleBuddy.API.Repositories.Implementations;
using System.Security.Claims;
using ToggleBuddy.API.Helpers;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/{projectId:Guid}/Feature")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
           private readonly IFeatureServices _featureServices;
           private readonly IProjectRepository _projectRepository;
           private readonly IUser _user;

        public FeatureController(IFeatureServices featureServices, IProjectRepository projectRepository,IUser user)
        {
            _featureServices = featureServices;
            _projectRepository = projectRepository;
            _user = user;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] Guid ProjectId ,[FromBody] FeatureRequestDto featureRequestDto)
        {
          // Fetch project using projectId and user from the repository
            var project = await _projectRepository.GetProjectByIdForCurrentUserAsync(ProjectId, User?.FindFirstValue(ClaimTypes.NameIdentifier));

            // Check if project was found and belongs to the current user
            if (project == null)
            {
                return NotFound("Project not found or not accessible to the current user.");
            }

            // Call the service method to update the feature
            var response = await _featureServices.CreateFeatureAsync(project, User, featureRequestDto);

            // Check the response status and return appropriate result
            if (response.Status == ResponseStatus.NotFound)
            {
                return NotFound(response.Message);
            }

            return Ok(response);

        }

        // [HttpGet("{id:Guid}")]
        // [Authorize]
        // public async Task<IActionResult> Show([FromRoute] Guid id)
        // {
           
        //     var ShowFeature = await _featureServices.ShowFeatureAsync(project, id, User);
        //     if(ShowFeature == null)
        //     return BadRequest("");
        //     return Ok(ShowFeature);
            
        // }

        // [HttpPut("{id:Guid}")]
        // [Authorize]
        // public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateFeatureRequestDto updateFeatureRequestDto)
        // {
        //     var updateFeature = await _featureServices.UpdateFeatureAsync(project,id,updateFeatureRequestDto,User);
        //     if(updateFeature == null)
        //     return BadRequest("");
        //     return Ok(updateFeature);
           
        // }

        // [HttpDelete("{id:Guid}")]
        // [Authorize]
        // public async Task<IActionResult> Delete([FromRoute] Guid id)
        // {
        //     var deleteFeature = await _featureServices.DeleteFeature(project,id, User);
        //     if(deleteFeature == null)return BadRequest("");
        //     return Ok(deleteFeature);
            
        // }
    }
}
