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
        //ApiResponse<string> _apiResponse
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
         
        }

        [HttpGet]
        [Authorize]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Show([FromRoute] Guid id, [FromRoute] Guid projectId )
        {
          
        }

        [HttpPut]
        [Authorize]
        [ValidateModel]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid projectId, [FromRoute] Guid id, [FromBody] UpdateFeatureRequestDto updateFeatureRequestDto)
        {
           
        }

        [HttpDelete]
        [Authorize]
        [ValidateModel]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid projectId, [FromRoute] Guid id )
        {
         

        }
    }
}
