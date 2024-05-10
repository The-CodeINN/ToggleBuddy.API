using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjectsAsync(string userId);
        Task<Project> CreateProjectAsync(Project project);
        Task<Project?> UpdateProjectAsync(Guid id, Project project, string userId);
        Task<Project?> DeleteProjectAsync(Guid id, string userId);
        Task<Project?> GetProjectByIdForAUserAsync(Guid id, string userId);
        Task<Project?> GetProjectByIdAsync(Guid id);
         
         //new added
         Task<Project?> GetWithFeaturesAsync(Guid id);
         Task<Project?> UpdateWithFeaturesAsync(Project project, Guid id);

        
    }
}
