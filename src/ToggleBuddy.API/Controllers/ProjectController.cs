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
                return CreatedAtAction(nameof(GetProjectById), new { id = response.Result.Id }, response);
            }
            else
            {
                return Utilities.HandleApiResponse(response);
            }
        }

        // GET List of projects: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var response = await _projectService.GetProjectsAsync(User);
            return Utilities.HandleApiResponse(response);
        }

        // GET: api/Project/{id}
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var response = await _projectService.GetProjectByIdAsync(id, User);
            return Utilities.HandleApiResponse(response);
        }

        // PUT: api/Project/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateProjectById(Guid id, [FromBody] ProjectRequestDto projectRequestDto)
        {
            var response = await _projectService.UpdateProjectAsync(id, projectRequestDto, User);
            return Utilities.HandleApiResponse(response);
        }

        // DELETE: api/Project/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProjectById(Guid id)
        {
            var response = await _projectService.DeleteProjectAsync(id, User);
            return Utilities.HandleApiResponse(response);
        }
    }
}