using System.Security.Claims;
using AutoMapper;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Repositories.Interfaces;

namespace ToggleBuddy.API.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IUser _userRepository;
        public ProjectService(IMapper mapper, IProjectRepository projectRepository, IUser userRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<ProjectResponseDto>> CreateProjectAsync(ProjectRequestDto projectRequestDto, ClaimsPrincipal userId)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(userId);
            if (currentUser == null) return new ApiResponse<ProjectResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };

            var project = _mapper.Map<Project>(projectRequestDto);
            project.UserId = currentUser.Id;

            var createdProject = await _projectRepository.CreateProjectAsync(project);
            if (createdProject == null) return new ApiResponse<ProjectResponseDto> { Message = "Project creation failed", Status = ResponseStatus.BadRequest };

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(createdProject);

            return new ApiResponse<ProjectResponseDto> { Result = projectResponseDto, Message = "Project created successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<ProjectResponseDto>> DeleteProjectAsync(Guid id, ClaimsPrincipal userId)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(userId);
            if (currentUser == null) return new ApiResponse<ProjectResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };

            var deletedProject = await _projectRepository.DeleteProjectAsync(id);
            if (deletedProject == null) return new ApiResponse<ProjectResponseDto> { Message = "Project not found", Status = ResponseStatus.NotFound };

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(deletedProject);

            return new ApiResponse<ProjectResponseDto> { Result = projectResponseDto, Message = "Project deleted successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<ProjectResponseDto>> GetProjectByIdForCurrentUserAsync(Guid id, ClaimsPrincipal userId)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(userId);
            if (currentUser == null) return new ApiResponse<ProjectResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };

            var project = await _projectRepository.GetProjectByIdForCurrentUserAsync(id, currentUser.Id);
            if (project == null) return new ApiResponse<ProjectResponseDto> { Message = "Project not found", Status = ResponseStatus.NotFound };

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(project);

            return new ApiResponse<ProjectResponseDto> { Result = projectResponseDto, Message = "Project retrieved successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<List<ProjectResponseDto>>> GetProjectsAsync(ClaimsPrincipal userId)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(userId);
            if (currentUser == null) return new ApiResponse<List<ProjectResponseDto>> { Message = "User not found", Status = ResponseStatus.NotFound };

            var projects = await _projectRepository.GetProjectsAsync();
            var projectResponseDtos = _mapper.Map<List<ProjectResponseDto>>(projects);

            return new ApiResponse<List<ProjectResponseDto>> { Result = projectResponseDtos, Message = "Projects retrieved successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<ProjectResponseDto>> UpdateProjectAsync(Guid id, ProjectRequestDto projectRequestDto, ClaimsPrincipal userId)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync(userId);
            if (currentUser == null) return new ApiResponse<ProjectResponseDto> { Message = "User not found", Status = ResponseStatus.NotFound };

            var project = _mapper.Map<Project>(projectRequestDto);
            var updatedProject = await _projectRepository.UpdateProjectAsync(id, project);
            if (updatedProject == null) return new ApiResponse<ProjectResponseDto> { Message = "Project not found", Status = ResponseStatus.NotFound };

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(updatedProject);

            return new ApiResponse<ProjectResponseDto> { Result = projectResponseDto, Message = "Project updated successfully", Status = ResponseStatus.Success };
        }
    }
}
