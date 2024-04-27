using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Services.FeatureEnvironmentServices;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/feature/{featureId:Guid}/environments")]
    [ApiController]
    public class FeatureEnvironmentController : ControllerBase
    {
        private readonly IFeatureEnvironmentServices _featureEnvironmentServices;


        public FeatureEnvironmentController(
            IFeatureEnvironmentServices featureEnvironmentServices
        )
        {
            _featureEnvironmentServices = featureEnvironmentServices;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFeatureEnvironmentAsync([FromRoute]Guid featureId, [FromBody] FeatureEnvironmentRequestDto featureEnvironmentRequestDto)
        {
            var response = await _featureEnvironmentServices.CreateFeatureEnvironmentAsync(featureId, featureEnvironmentRequestDto);
            if (response.Status == ResponseStatus.Success && response.Result != null){
                return CreatedAtAction(nameof(GetFeatureEnvironmentByIdAsync), new { featureId, id = response.Result.Id }, response);
            } else {
                return Utilities.HandleApiResponse(response);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowAllFeatureEnvironmentsAsync([FromRoute]Guid featureId)
        {
            var response = await _featureEnvironmentServices.ShowAllFeatureEnvironmentsAsync(featureId);
            return Utilities.HandleApiResponse(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> GetFeatureEnvironmentByIdAsync([FromRoute]Guid featureId, [FromRoute]Guid id)
        {
            var response = await _featureEnvironmentServices.GetFeatureEnvironmentByIdAsync(featureId, id);
            return Utilities.HandleApiResponse(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateFeatureEnvironmentAsync([FromRoute]Guid featureId, [FromRoute]Guid id, [FromBody] FeatureEnvironmentRequestDto featureEnvironmentRequestDto)
        {
            var response = await _featureEnvironmentServices.UpdateFeatureEnvironmentAsync(featureId, id, featureEnvironmentRequestDto);
            return Utilities.HandleApiResponse(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteFeatureEnvironment([FromRoute]Guid featureId, [FromRoute]Guid id)
        {
            var response = await _featureEnvironmentServices.DeleteFeatureEnvironmentAsync(featureId, id);
            return Utilities.HandleApiResponse(response);
        }
    }
}