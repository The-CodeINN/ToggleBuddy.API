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

        // POST: api/Project
        [HttpPost]
       
        [ValidateModel]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequestDto projectRequestDto)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(User);
            if (currentUser == null)
            {
                _apiResponse.Message = "User not found";
                _apiResponse.Result = null;
                _apiResponse.Status = ResponseStatus.Error;
                return NotFound(_apiResponse);
            }

            var project = _mapper.Map<Project>(projectRequestDto);
            project.User = currentUser;

            project = await _projectRepository.CreateProjectAsync(project);

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(project);

            _apiResponse.Result = projectResponseDto;
            _apiResponse.Message = "Project created successfully";

            return CreatedAtAction(nameof(CreateProject), new { id = project.Id }, _apiResponse);
        }

        // GET List of projects: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(User);
            if (currentUser == null)
            {
                _apiResponse.Message = "User not found";
                _apiResponse.Result = null;
                _apiResponse.Status = ResponseStatus.Error;
                return NotFound(_apiResponse);
            }

            var projects = await _projectRepository.GetProjectsAsync();
            projects = currentUser.Projects;

            var projectResponseDto = _mapper.Map<List<ProjectResponseDto>>(projects);

            _apiResponse.Result = projectResponseDto;
            _apiResponse.Message = "Projects retrieved successfully";

            return Ok(_apiResponse);
        }

        // GET: api/Project/5
        [HttpGet]
        [Route("{id:Guid}")]


        public async Task<IActionResult> GetProjectById([FromRoute] Guid id)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(User);
            if (currentUser == null)
            {
                _apiResponse.Message = "User not found";
                _apiResponse.Result = null;
                _apiResponse.Status = ResponseStatus.Error;
                return NotFound(_apiResponse);
            }

            var project = await _projectRepository.GetProjectByIdForCurrentUserAsync(id, (currentUser.Id));
            if (project == null)
            {
                _apiResponse.Message = "Project not found";
                _apiResponse.Result = null;
                _apiResponse.Status = ResponseStatus.Error;
                return NotFound(_apiResponse);
            }

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(project);

            _apiResponse.Result = projectResponseDto;
            _apiResponse.Message = "Project retrieved successfully";

            return Ok(_apiResponse);
        }


        // PUT: api/Project/5
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProjectById([FromRoute] Guid id, [FromBody] ProjectRequestDto projectRequestDto)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(User);
            if (currentUser == null)
            {
                _apiResponse.Message = "User not found";
                _apiResponse.Result = null;
                _apiResponse.Status = ResponseStatus.Error;
                return NotFound(_apiResponse);
            }

            var project = await _projectRepository.GetProjectByIdForCurrentUserAsync(id, (currentUser.Id));
            if (project == null)
            {
                _apiResponse.Message = "Project not found";
                _apiResponse.Result = null;
                _apiResponse.Status = ResponseStatus.Error;
                return NotFound(_apiResponse);
            }

            var updatedProject = _mapper.Map<Project>(projectRequestDto);
            updatedProject.Id = project.Id;

            var result = await _projectRepository.UpdateProjectAsync(id, updatedProject);

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(result);

            _apiResponse.Result = projectResponseDto;
            _apiResponse.Message = "Project updated successfully";

            return Ok(_apiResponse);
        }

        // DELETE: api/Project/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProjectById([FromRoute] Guid id)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(User);
            if (currentUser == null)
            {
                _apiResponse.Message = "User not found";
                _apiResponse.Result = null;
                _apiResponse.Status = ResponseStatus.Error;
                return NotFound(_apiResponse);
            }

            var project = await _projectRepository.GetProjectByIdForCurrentUserAsync(id, (currentUser.Id));
            if (project == null)
            {
                _apiResponse.Message = "Project not found";
                _apiResponse.Result = null;
                _apiResponse.Status = ResponseStatus.Error;
                return NotFound(_apiResponse);
            }

            var result = await _projectRepository.DeleteProjectAsync(id);

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(result);

            _apiResponse.Result = projectResponseDto;
            _apiResponse.Message = "Project deleted successfully";

            return Ok(_apiResponse);
        }

    }
}
