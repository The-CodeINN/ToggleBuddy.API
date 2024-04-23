using Microsoft.EntityFrameworkCore;
using ToggleBuddy.API.Data;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Repositories.Interfaces;

namespace ToggleBuddy.API.Repositories.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ToggleBuddyDbContext _dbContext;

        public ProjectRepository(ToggleBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            return project;
        }

        public async Task<Project?> DeleteProjectAsync(Guid id)
        {
            var existingProject = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (existingProject == null)
            {
                return null;
            }

            _dbContext.Projects.Remove(existingProject);
            await _dbContext.SaveChangesAsync();

            return existingProject;
        }


        // Refactor this method and use the user claims instead of passing the userId as a parameter
        public async Task<Project?> GetProjectByIdForCurrentUserAsync(Guid id, string? userId)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            if (project?.UserId != userId)
                return null; // If the project does not belong to the current user, return null
            return project;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project?> UpdateProjectAsync(Guid id, Project project)
        {
            var existingProject = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (existingProject == null)
            {
                return null;
            }

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;

            await _dbContext.SaveChangesAsync();
            return existingProject;
        }
    }
}