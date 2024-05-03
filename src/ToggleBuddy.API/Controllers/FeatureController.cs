using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Repositories.Interfaces;
using ToggleBuddy.API.Repositories.Implementations;
using System.Security.Claims;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Services.FeatureServices;

namespace ToggleBuddy.API.Controllers
{
  [Route("api/Feature")]
  [ApiController]
  public class FeatureController(
    FeatureServices featureServices
  ) : ControllerBase
  {

    [HttpPost]
    [Authorize]

    public async Task<IActionResult> CreateFeature([FromRoute] Guid projectId, [FromBody] FeatureRequestDto featureRequestDto)
    {
      var res = await featureServices.CreateFeatureAsync(featureRequestDto,projectId);
      if(res.Status == ResponseStatus.Success && res.Result!= null)
      {
        return CreatedAtAction(nameof(GetFeatureDetailsByProjectId), new {projectId, id =res.Result.Id}, res);
      }
      else
      {
        return Utilities.HandleApiResponse(res);
      }
    }
  

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetFeatureDetailsByProjectId([FromRoute] Guid projectId, [FromRoute] Guid id)
    {
      var response = await featureServices.GetFeatureDetailsByProjectIdAsync(projectId, id);
      return Utilities.HandleApiResponse(response);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetFeatures([FromRoute] Guid projectId)
    {
      var response = await featureServices.GetFeaturesByProjectIdAsync(projectId);
      return Utilities.HandleApiResponse(response);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateFeature([FromRoute] Guid id, [FromRoute] Guid projectId, [FromBody] UpdateFeatureRequestDto updateFeatureRequestDto) 
    {
      var response = await featureServices.UpdateFeatureAsync(updateFeatureRequestDto, projectId, id);
      return Utilities.HandleApiResponse(response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]  
      public async Task<IActionResult> Delete([FromRoute] Guid projectId, [FromRoute] Guid id)
    {
      var response = await featureServices.DeleteFeatureAsync(projectId, id);
      return Utilities.HandleApiResponse(response);

    }
  }
}

    
  

