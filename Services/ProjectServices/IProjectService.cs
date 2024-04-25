using System.Security.Claims;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Services.ProjectServices
{

    public interface IProjectService
    {
        Task<ServiceResponse<ProjectResponseDto>> CreateProjectAsync(ProjectRequestDto project, ClaimsPrincipal claimsPrincipal);
        Task<ServiceResponse<ProjectResponseDto>> DeleteProjectAsync(Guid id, ClaimsPrincipal claimsPrincipal);
        Task<ServiceResponse<ProjectResponseDto>> GetProjectByIdAsync(Guid id, ClaimsPrincipal claimsPrincipal);
        Task<ServiceResponse<List<ProjectResponseDto>>> GetProjectsAsync(ClaimsPrincipal claimsPrincipal);
        Task<ServiceResponse<ProjectResponseDto>> UpdateProjectAsync(Guid id, ProjectRequestDto project, ClaimsPrincipal claimsPrincipal);
    }
}
