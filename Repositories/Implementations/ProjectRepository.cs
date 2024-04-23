﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Project?> DeleteProjectAsync(Guid id, string userId)
        {
            var projectToDelete = await _dbContext.Projects.FindAsync([id]);
            if (projectToDelete == null || projectToDelete.UserId != userId)
                return null;
            _dbContext.Projects.Remove(projectToDelete);
            await _dbContext.SaveChangesAsync();

            return projectToDelete;
        }


        public async Task<Project?> GetProjectByIdAsync(Guid id, string userId)
        {
            var project = await _dbContext.Projects.FindAsync([id]);
            return project?.UserId == userId ? project : null;
        }

        public async Task<List<Project>> GetProjectsAsync(string userId)
        {
            return await _dbContext.Projects.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Project?> UpdateProjectAsync(Guid id, Project project, string userId)
        {
            var existingProject = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (existingProject == null)
                return null;

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;

            await _dbContext.SaveChangesAsync();
            return existingProject;
        }

    }
}