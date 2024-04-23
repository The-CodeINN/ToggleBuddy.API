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
  [Route("api/{projectId:Guid}/Feature")]
  [ApiController]
  public class FeatureController : ControllerBase
  {
    private readonly IFeatureServices _featureServices;
    public FeatureController(IFeatureServices featureServices)
    {
      _featureServices = featureServices;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] Guid projectId, [FromBody] FeatureRequestDto featureRequestDto)
    {
      var response = await _featureServices.CreateFeatureAsync(featureRequestDto, projectId);
      if (response.Status == ResponseStatus.Success && response.Result != null)
      {
        return CreatedAtAction(nameof(GetFeatureDetailsByProjectId), new { projectId, id = response.Result.Id }, response);
      }
      else
      {
        return HandleApiResponse(response);
      }
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetFeatureDetailsByProjectId([FromRoute] Guid projectId, [FromRoute] Guid id)
    {
      var response = await _featureServices.GetFeatureDetailsByProjectIdAsync(projectId, id);
      return HandleApiResponse(response);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetFeatures([FromRoute] Guid projectId)
    {
      var response = await _featureServices.GetFeaturesByProjectIdAsync(projectId);
      return HandleApiResponse(response);
    }


    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateFeature([FromRoute] Guid projectId, [FromRoute] Guid id, [FromBody] UpdateFeatureRequestDto updateFeatureRequestDto)
    {
      var response = await _featureServices.UpdateFeatureAsync(updateFeatureRequestDto, projectId, id);
      return HandleApiResponse(response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid projectId, [FromRoute] Guid id)
    {
      var response = await _featureServices.DeleteFeatureAsync(projectId, id);
      return HandleApiResponse(response);
    }

    private IActionResult HandleApiResponse<T>(ApiResponse<T> response)
    {
      return response.Status switch
      {
        ResponseStatus.Success => Ok(response),
        ResponseStatus.NotFound => NotFound(response),
        ResponseStatus.BadRequest => BadRequest(response),
        _ => StatusCode(500, response)
      };
    }
  }
}
