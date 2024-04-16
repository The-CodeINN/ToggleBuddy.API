using Microsoft.EntityFrameworkCore;
using ToggleBuddy.API.Data;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Respositories.Interfaces;

namespace ToggleBuddy.API.Respositories.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ToggleBuddyDbContext dbContext;

        public ProjectRepository(ToggleBuddyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();

            return project;
        }

        public async Task<Project?> DeleteProjectAsync(Guid id)
        {
            var existingProject = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (existingProject == null)
            {
                return null;
            }

            dbContext.Projects.Remove(existingProject);
            await dbContext.SaveChangesAsync();

            return existingProject;
        }

       

        public async Task<Project?> GetProjectByIdForCurrentUserAsync(Guid id, Guid userId)
        {
           // await Console.Out.WriteLineAsync(userId);
            var project = await dbContext.Projects.FirstOrDefaultAsync(p=>p.Id==id);
            if (project?.UserId != userId)
                return null; // If the project does not belong to the current user, return null
            return project;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await dbContext.Projects.ToListAsync();
        }

        public async Task<Project?> UpdateProjectAsync(Guid id, Project project)
        {
            var existingProject = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (existingProject == null)
            {
                return null;
            }

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;

            await dbContext.SaveChangesAsync();
            return existingProject;
        }
    }
}
