using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Services.FeatureServices;

namespace ToggleBuddy.API.Controllers
{
  [Route("api/Feature")]
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

    public async Task<IActionResult> CreateFeature( [FromBody] FeatureRequestDto featureRequestDto)
    {
      var response = await _featureServices.CreateFeatureAsync(featureRequestDto);
      if(response.Status == ResponseStatus.Success && response.Result!= null)
      {
        return CreatedAtAction(nameof(GetFeatureDetailsByProjectId), new {id =response.Result.Id}, response);
      }
      else
      {
        return Utilities.HandleApiResponse(response);
      }
    }

      [HttpPost]
      [Route("{projectId:Guid}")]
      [Authorize]

      public async Task <IActionResult> GetFeature([FromRoute] Guid projectId, [FromBody] FeatureRequestModel model)
      {
        var response = await _featureServices.GetFeatureByProjectId(projectId);
        return Utilities.HandleApiResponse(response);
      }
  

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetFeatureDetailsByProjectId([FromRoute]  Guid projectId)
    {
      var response = await _featureServices.GetFeatureDetailsByProjectIdAsync(projectId);
      return Utilities.HandleApiResponse(response);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateFeature([FromRoute] Guid id,  [FromBody] UpdateFeatureRequestDto updateFeatureRequestDto) 
    {
      var response = await _featureServices.UpdateFeatureAsync(updateFeatureRequestDto, id);
      return Utilities.HandleApiResponse(response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]  
      public async Task<IActionResult> Delete([FromRoute] Guid projectId, [FromRoute] Guid id)
    {
      var response = await _featureServices.DeleteFeatureAsync(projectId, id);
      return Utilities.HandleApiResponse(response);

    }
  }
}

    
  

