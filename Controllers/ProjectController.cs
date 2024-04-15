using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Respositories.Interfaces;

namespace ToggleBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProjectRespository projectRepository;
        private readonly IUser userRepsitory;
        private readonly ApiResponse<object> apiResponse = new ApiResponse<object>();

        public ProjectController(IMapper mapper, IProjectRespository projectRepository, IUser userRepsitory)
        {
            this.mapper = mapper;
            this.projectRepository = projectRepository;
            this.userRepsitory = userRepsitory;
        }

        // POST: api/Project
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequestDto projectRequestDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var user = await userRepsitory.GetUserByIdAsync(userId);

            if (user == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "User not found";
                return NotFound(apiResponse);
            }

            var project = mapper.Map<Project>(projectRequestDto);
            project.UserId = Guid.Parse(userId);

            project = await projectRepository.CreateProjectAsync(project);

            var projectResponseDto = mapper.Map<ProjectResponseDto>(project);

            apiResponse.Status = ResponseStatus.Success;
            apiResponse.Result = projectResponseDto;
            apiResponse.Message = "Project created successfully";

            return CreatedAtAction(nameof(CreateProject), new { id = project.Id }, apiResponse);

        }

        // GET List of project: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var user = await userRepsitory.GetUserByIdAsync(userId);

            if (user == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "User not found";
                return NotFound(apiResponse);
            }

            var projects = await projectRepository.GetProjectsAsync();

            var projectResponseDto = mapper.Map<List<ProjectResponseDto>>(projects);

            apiResponse.Status = ResponseStatus.Success;
            apiResponse.Result = projectResponseDto;
            apiResponse.Message = "Projects retrieved successfully";

            return Ok(apiResponse);
        }

        // GET: api/Project/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProjectById([FromRoute] Guid id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var user = await userRepsitory.GetUserByIdAsync(userId);

            if (user == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "User not found";
                return NotFound(apiResponse);
            }

            var project = await projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                 apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "Project not found";
                return NotFound(apiResponse);
            }

            var projectResponseDto = mapper.Map<ProjectResponseDto>(project);
            apiResponse.Status = ResponseStatus.Success;
            apiResponse.Result = projectResponseDto;
            apiResponse.Message = "Project retrieved successfully";
            
            return Ok(apiResponse);
        }

        // PUT: api/Project/5
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProjectById([FromRoute] Guid id, [FromBody] ProjectRequestDto projectRequestDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var user = await userRepsitory.GetUserByIdAsync(userId);

            if (user == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "User not found";
                return NotFound(apiResponse);
            }

            var project = await projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "Project not found";
                return NotFound(apiResponse);
            }

            var updatedProject = mapper.Map<Project>(projectRequestDto);
            updatedProject.Id = id;
            updatedProject.UserId = Guid.Parse(userId);

            var result = await projectRepository.UpdateProjectAsync(id, updatedProject);

            if (result == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "Project not found";
                return NotFound(apiResponse);
            }

            var projectResponseDto = mapper.Map<ProjectResponseDto>(result);

            apiResponse.Status = ResponseStatus.Success;
            apiResponse.Result = projectResponseDto;
            apiResponse.Message = "Project updated successfully";

            return Ok(apiResponse);
        }

        // DELETE: api/Project/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProjectById([FromRoute] Guid id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var user = await userRepsitory.GetUserByIdAsync(userId);

            if (user == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "User not found";
                return NotFound(apiResponse);
            }

            var project = await projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "Project not found";
                return NotFound(apiResponse);
            }

            var result = await projectRepository.DeleteProjectAsync(id);

            if (result == null)
            {
                apiResponse.Status = ResponseStatus.Error;
                apiResponse.Message = "Project not found";
                return NotFound(apiResponse);
            }

            var projectResponseDto = mapper.Map<ProjectResponseDto>(result);

            apiResponse.Status = ResponseStatus.Success;
            apiResponse.Result = projectResponseDto;
            apiResponse.Message = "Project deleted successfully";

            return Ok(apiResponse);
        }
    }

}

