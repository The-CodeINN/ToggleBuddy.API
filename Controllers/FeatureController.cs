// using AutoMapper;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using ToggleBuddy.API.Services.Interfaces;
// using ToggleBuddy.API.Helpers;
// using ToggleBuddy.API.Models.Domain;
// using ToggleBuddy.API.Models.DTOs.RequestDTOs;
// using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
// using ToggleBuddy.API.Repositories.Interfaces;


// namespace ToggleBuddy.API.Controllers
// {
//     [Route("api/{projectId:Guid}/Feature")]
//     [ApiController]
//     public class FeatureController(
//         IFeatureServices featureServices,
//          IUser user
//         ) : ControllerBase
//     {    
//         [HttpPost]
//         [Authorize]
//         //[ValidateModel]
//         public async Task<IActionResult> Create(
//             [FromRoute] Guid projectId,
//             [FromBody] FeatureRequestDto featureRequestDto)
//         {
//             //call the service layer method to create the feature
//             var FeatureCreated = await featureServices.CreateFeatureAsync(projectId, featureRequestDto);
//             if(FeatureCreated == null)
//                   return BadRequest("Feature creation failed"); 
//             return Ok();     
//         }
//         [HttpGet]
//         [Authorize]
//        // [ValidateModel]
//         [Route("{id:Guid}")]
//         public async Task<IActionResult> Show([FromRoute] Guid id, [FromRoute] Guid projectId )
//         {
//              //call the service layer method to show feature by ID
//              var ShowFeature = await featureServices.ShowFeatureAsync(project, id);
//              // if project does not exist or feature doesnt belong to the project/user
//              if(ShowFeature == null)
//                 return BadRequest("Feature details failed");
//              return Ok();
//         }
//         [HttpPut]
//         [Authorize]
//        // [ValidateModel]
//         [Route("{id:guid}")]
//         public async Task<IActionResult> Update([FromRoute] Guid projectId, [FromRoute] Guid id, [FromBody] UpdateFeatureRequestDto updateFeatureRequestDto)
//         {
//             //call the Update Service layer method
//             var UpdateFeature = await featureServices.UpdateFeatureAsync(project, id, feature);
//             // if is not authenticated, the project doesnt exit or belong to the feature
//             if(UpdateFeature == null)
//                 return BadRequest("Feature update failed");
//                 return Ok();   
//         }
//         [HttpDelete]
//         [Authorize]
//       //  [ValidateModel]
//         [Route("{id:Guid}")]
//         public async Task<IActionResult> Delete([FromRoute] Guid projectId, [FromRoute] Guid id )
//         {
//             var DeleteFeature = await featureServices.DeleteFeature(project,id);
//             if(DeleteFeature == null)
//             return BadRequest("Feature delection failed");
//             return Ok();
//         }
//     }
// }
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Services.Interfaces;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/{projectId:Guid}/Feature")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureServices _featureServices;
       // private readonly IUser _user;

        public FeatureController(IFeatureServices featureServices)
        {
            _featureServices = featureServices;
          //  _user = user;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Guid projectId, FeatureRequestDto featureRequestDto)
        {
            var featureCreated = await _featureServices.CreateFeatureAsync(projectId, featureRequestDto);
            if (featureCreated == null)
                return BadRequest("Feature creation failed");
            return Ok();
        }

        [HttpGet("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Show(Guid projectId, Guid id)
        {
            var feature = await _featureServices.ShowFeatureAsync(project, id);
            if (feature == null)
                return BadRequest("Feature details failed");
            return Ok();
        }

        [HttpPut("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid projectId, Guid id, UpdateFeatureRequestDto updateFeatureRequestDto)
        {
            var updatedFeature = await _featureServices.UpdateFeatureAsync(project, id, updateFeatureRequestDto);
            if (updatedFeature == null)
                return BadRequest("Feature update failed");
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid projectId, Guid id)
        {
            var deleteFeature = await _featureServices.DeleteFeature(project, id);
            if (deleteFeature == null)
                return BadRequest("Feature deletion failed");
            return Ok();
        }
    }
}
