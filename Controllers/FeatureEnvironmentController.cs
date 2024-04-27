using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Services.FeatureEnvironmentServices;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/{featureId:Guid}/Environment")]
    [ApiController]
    public class FeatureEnvironmentController : ControllerBase
    {
        private readonly IFeatureEnvironmentServices _featureEnvironmentServices;

        // constructor
        public FeatureEnvironmentController(IFeatureEnvironmentServices featureEnvironmentServices)
        {
            _featureEnvironmentServices = featureEnvironmentServices;
        }

        [HttpPost]
        [Authorize]
        // creates a new feature environment for a given feature
        public async Task<IActionResult> CreateFeatureEnvironment([FromRoute]Guid featureId, [FromBody] FeatureEnvironmentRequestDto featureEnvironmentRequestDto)
        {
            var response = await _featureEnvironmentServices.CreateFeatureEnvironmentAsync(featureEnvironmentRequestDto, featureId);
            if (response.Status == ResponseStatus.Success && response.Result != null){
                return CreatedAtAction(nameof(GetFeatureEnvironmentById), new { featureId = featureId, id = response.Result.Id }, response);
            } else {
                return Utilities.HandleApiResponse(response);
            }
        }

        [HttpGet]
        [Authorize]
        // returns all feature environments for a given feature
        public async Task<IActionResult> ShowAllFeatureEnvironments([FromRoute]Guid featureId)
        {
            var response = await _featureEnvironmentServices.ShowAllFeatureEnvironmentsAsync(featureId);
            return Utilities.HandleApiResponse(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize]
        // returns a single feature environment for a given feature
        public async Task<IActionResult> GetFeatureEnvironmentById([FromRoute]Guid featureId, [FromRoute]Guid id)
        {
            var response = await _featureEnvironmentServices.GetFeatureEnvironmentByIdAsync(featureId, id);
            return Utilities.HandleApiResponse(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        // updates a single feature environment for a given feature
        public async Task<IActionResult> UpdateFeatureEnvironment([FromRoute]Guid featureId, [FromRoute]Guid id, [FromBody] FeatureEnvironmentRequestDto featureEnvironmentRequestDto)
        {
            var response = await _featureEnvironmentServices.UpdateFeatureEnvironmentAsync(featureId, id, featureEnvironmentRequestDto);
            return Utilities.HandleApiResponse(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        // deletes a single feature environment for a given feature
        public async Task<IActionResult> DeleteFeatureEnvironment([FromRoute]Guid featureId, [FromRoute]Guid id)
        {
            var response = await _featureEnvironmentServices.DeleteFeatureEnvironmentAsync(featureId, id);
            return Utilities.HandleApiResponse(response);
        }
    }
}