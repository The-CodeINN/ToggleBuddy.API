using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Services.ProjectServices;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequestDto projectRequestDto)
        {
            var response = await _projectService.CreateProjectAsync(projectRequestDto, User);
            if (response.Status == ResponseStatus.Success && response.Result != null)
            {
                return CreatedAtAction(nameof(GetProjectById), new { id = response.Result.Id }, response.Result);
            }
            else
            {
                return HandleApiResponse(response);
            }
        }

        // GET List of projects: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var response = await _projectService.GetProjectsAsync(User);
            return HandleApiResponse(response);
        }

        // GET: api/Project/{id}
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var response = await _projectService.GetProjectByIdForCurrentUserAsync(id, User);
            return HandleApiResponse(response);
        }

        // PUT: api/Project/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateProjectById(Guid id, [FromBody] ProjectRequestDto projectRequestDto)
        {
            var response = await _projectService.UpdateProjectAsync(id, projectRequestDto, User);
            return HandleApiResponse(response);
        }

        // DELETE: api/Project/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProjectById(Guid id)
        {
            var response = await _projectService.DeleteProjectAsync(id, User);
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