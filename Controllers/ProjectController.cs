using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Respositories.Interfaces;
using ToggleBuddy.API.Models.DTOs;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IUser _userRepository;
        private readonly ApiResponse<object> _apiResponse = new ApiResponse<object>();

        public ProjectController(IMapper mapper, IProjectRepository projectRepository, IUser userRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        private async Task<User?> GetCurrentUser()
        {
            return await _userRepository.GetCurrentUserAsync(User);
        }

        private IActionResult ApiResponse(object result, string message, ResponseStatus status)
        {
            _apiResponse.Result = result;
            _apiResponse.Message = message;
            _apiResponse.Status = status;

            return Ok(_apiResponse);
        }

        // POST: api/Project
        [HttpPost]
       
        [ValidateModel]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequestDto projectRequestDto)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null) return NotFound("User not found");

            var project = _mapper.Map<Project>(projectRequestDto);
            project.UserId = currentUser.Id;

            var createdProject = await _projectRepository.CreateProjectAsync(project);
            if (createdProject == null) return BadRequest("Project creation failed");

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(createdProject);

            _apiResponse.Result = projectResponseDto;
            _apiResponse.Message = "Project created successfully";
            return CreatedAtAction(nameof(GetProjectById),
                new { id = createdProject.Id }, _apiResponse);
        }

        // GET List of projects: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null) return NotFound("User not found");

            var projects = await _projectRepository.GetProjectsAsync();
            projects = currentUser.Projects;

            var projectResponseDto = _mapper.Map<List<ProjectResponseDto>>(projects);

            return ApiResponse(projectResponseDto, "Projects retrieved successfully", ResponseStatus.Success);
        }

        // GET: api/Project/5
        [HttpGet]
        [Route("{id:Guid}")]


        public async Task<IActionResult> GetProjectById([FromRoute] Guid id)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null) return NotFound("User not found");

            var project = await _projectRepository.GetProjectByIdForCurrentUserAsync(id, currentUser.Id);
            if (project == null) return NotFound("Project not found");

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(project);

            return ApiResponse(projectResponseDto, "Project retrieved successfully", ResponseStatus.Success);

        }


        // PUT: api/Project/5
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProjectById([FromRoute] Guid id, [FromBody] ProjectRequestDto projectRequestDto)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null) return NotFound("User not found");

            var project = await _projectRepository.GetProjectByIdForCurrentUserAsync(id, currentUser.Id);
            if (project == null) return NotFound("Project not found");

            var updatedProject = _mapper.Map<Project>(projectRequestDto);

            var result = await _projectRepository.UpdateProjectAsync(id, updatedProject);
            if (result == null) return BadRequest("Project update failed");

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(result);

            return ApiResponse(projectResponseDto, "Project updated successfully", ResponseStatus.Success);
        }

        // DELETE: api/Project/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProjectById([FromRoute] Guid id)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null) return NotFound("User not found");

            var project = await _projectRepository.GetProjectByIdForCurrentUserAsync(id, currentUser.Id);
            if (project == null) return NotFound("Project not found");

            var result = await _projectRepository.DeleteProjectAsync(project.Id);
            if (result == null) return BadRequest("Project deletion failed");

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(result);

            return ApiResponse(projectResponseDto, "Project deleted successfully", ResponseStatus.Success);
        }
    }
}
