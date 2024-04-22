using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjectsAsync();
        Task<Project> CreateProjectAsync(Project project);
        Task<Project?> UpdateProjectAsync(Guid id, Project project);
        Task<Project?> DeleteProjectAsync(Guid id);
        Task<Project?> GetProjectByIdForCurrentUserAsync(Guid id, string? userId);

    }
}
