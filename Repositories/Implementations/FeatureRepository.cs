using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToggleBuddy.API.Data;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Repositories.Interfaces;

namespace ToggleBuddy.API.Repositories.Implementations
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ToggleBuddyDbContext _dbContext;

        public FeatureRepository(ToggleBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Feature> CreateAsync(Feature feature, Guid projectId)
        {
            feature.ProjectId = projectId; // Ensure the feature is linked to the correct project
            await _dbContext.Features.AddAsync(feature);
            await _dbContext.SaveChangesAsync();
            return feature;
        }

        public async Task<Feature?> ShowAsync(Guid projectId, Guid featureId)
        {
            return await _dbContext.Features
                .Where(f => f.Id == featureId && f.ProjectId == projectId)
                .FirstOrDefaultAsync();
        }

        public async Task<Feature?> UpdateAsync(Feature feature, Guid projectId, Guid featureId)
        {
            var existingFeature = await _dbContext.Features
                .FirstOrDefaultAsync(f => f.Id == featureId && f.ProjectId == projectId);

            if (existingFeature == null)
            {
                throw new KeyNotFoundException("Feature not found.");
            }

            // Update properties
            existingFeature.Name = feature.Name;
            existingFeature.Description = feature.Description;
            existingFeature.ExpirationDate = feature.ExpirationDate;
            existingFeature.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return existingFeature;
        }

        public async Task<ICollection<Feature>> GetAllAsync(Guid projectId)
        {
            return await _dbContext.Features
                .Where(f => f.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<Feature?> DeleteAsync(Guid projectId, Guid featureId)
        {
            var featureToDelete = await _dbContext.Features
                .FirstOrDefaultAsync(f => f.Id == featureId && f.ProjectId == projectId);

            if (featureToDelete == null)
            {
                return null;
            }

            _dbContext.Features.Remove(featureToDelete);
            await _dbContext.SaveChangesAsync();
            return featureToDelete;
        }
    }
}