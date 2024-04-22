using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public ProjectService(IMapper mapper, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<ProjectResponseDto>> CreateProjectAsync(ProjectRequestDto projectRequestDto, ClaimsPrincipal claimsPrincipal)
        {
            var currentUserId = claimsPrincipal.GetLoggedInUserId();
            var project = _mapper.Map<Project>(projectRequestDto);
            project.UserId = currentUserId;

            var createdProject = await _projectRepository.CreateProjectAsync(project);
            var projectResponseDto = _mapper.Map<ProjectResponseDto>(createdProject);

            return new ApiResponse<ProjectResponseDto> { Result = projectResponseDto, Message = "Project created successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<ProjectResponseDto>> DeleteProjectAsync(Guid id, ClaimsPrincipal claimsPrincipal)
        {
            var currentUserId = claimsPrincipal.GetLoggedInUserId();

            var deletedProject = await _projectRepository.DeleteProjectAsync(id, currentUserId);
            if (deletedProject == null)
                return new ApiResponse<ProjectResponseDto> { Message = "Project not found or you are not authorized to delete it", Status = ResponseStatus.NotFound };

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(deletedProject);
            return new ApiResponse<ProjectResponseDto> { Result = projectResponseDto, Message = "Project deleted successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<ProjectResponseDto>> GetProjectByIdAsync(Guid id, ClaimsPrincipal claimsPrincipal)
        {
            var currentUserId = claimsPrincipal.GetLoggedInUserId();

            var project = await _projectRepository.GetProjectByIdAsync(id, currentUserId);
            if (project == null)
                return new ApiResponse<ProjectResponseDto> { Message = "Project not found or you are not authorized to access it", Status = ResponseStatus.NotFound };

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(project);
            return new ApiResponse<ProjectResponseDto> { Result = projectResponseDto, Message = "Project retrieved successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<List<ProjectResponseDto>>> GetProjectsAsync(ClaimsPrincipal claimsPrincipal)
        {
            var currentUserId = claimsPrincipal.GetLoggedInUserId();

            var projects = await _projectRepository.GetProjectsAsync(currentUserId);
            var projectResponseDtos = _mapper.Map<List<ProjectResponseDto>>(projects);

            return new ApiResponse<List<ProjectResponseDto>> { Result = projectResponseDtos, Message = "Projects retrieved successfully", Status = ResponseStatus.Success };
        }

        public async Task<ApiResponse<ProjectResponseDto>> UpdateProjectAsync(Guid id, ProjectRequestDto projectRequestDto, ClaimsPrincipal claimsPrincipal)
        {
            var currentUserId = claimsPrincipal.GetLoggedInUserId();

            var result = await _projectRepository.UpdateProjectAsync(id, _mapper.Map<Project>(projectRequestDto), currentUserId);
            if (result == null)
                return new ApiResponse<ProjectResponseDto> { Message = "Project not found or you are not authorized to update it", Status = ResponseStatus.NotFound };

            var projectResponseDto = _mapper.Map<ProjectResponseDto>(result);
            return new ApiResponse<ProjectResponseDto> { Result = projectResponseDto, Message = "Project updated successfully", Status = ResponseStatus.Success };
        }
    }
}
