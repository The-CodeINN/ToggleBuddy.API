using System.Security.Claims;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Services.ProjectServices
{

    public interface IProjectService
    {
        Task<ApiResponse<ProjectResponseDto>> CreateProjectAsync(ProjectRequestDto project, ClaimsPrincipal userId);
        Task<ApiResponse<ProjectResponseDto>> DeleteProjectAsync(Guid id, ClaimsPrincipal userId);
        Task<ApiResponse<ProjectResponseDto>> GetProjectByIdForCurrentUserAsync(Guid id, ClaimsPrincipal userId);
        Task<ApiResponse<List<ProjectResponseDto>>> GetProjectsAsync(ClaimsPrincipal userId);
        Task<ApiResponse<ProjectResponseDto>> UpdateProjectAsync(Guid id, ProjectRequestDto project, ClaimsPrincipal userId);
    }
}
