using System.Security.Claims;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Services.ProjectServices
{

    public interface IProjectService
    {
        Task<ApiResponse<ProjectResponseDto>> CreateProjectAsync(ProjectRequestDto project, ClaimsPrincipal claimsPrincipal);
        Task<ApiResponse<ProjectResponseDto>> DeleteProjectAsync(Guid id, ClaimsPrincipal claimsPrincipal);
        Task<ApiResponse<ProjectResponseDto>> GetProjectByIdAsync(Guid id, ClaimsPrincipal claimsPrincipal);
        Task<ApiResponse<List<ProjectResponseDto>>> GetProjectsAsync(ClaimsPrincipal claimsPrincipal);
        Task<ApiResponse<ProjectResponseDto>> UpdateProjectAsync(Guid id, ProjectRequestDto project, ClaimsPrincipal claimsPrincipal);
    }
}
