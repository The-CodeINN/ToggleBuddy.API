using Microsoft.EntityFrameworkCore;
using ToggleBuddy.API.Data;
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

        public async Task<Feature> CreateAsync(Feature feature)
        {
            // feature.ProjectId = projectId; // Ensure the feature is linked to the correct project
            await _dbContext.Features.AddAsync(feature);
            await _dbContext.SaveChangesAsync();
            return feature;
        }

        public async Task<Feature> ShowAsync(Guid featureId)
        {
            var feature = await _dbContext.Features.FirstOrDefaultAsync(f=>f.Id==featureId);
            if (feature == null)
            {
                throw new ArgumentException("feature not found");
            }
            // Check if the feature belongs to a project by verifying that ProjectId is not empty
            if (feature.ProjectId == Guid.Empty)
            {
                throw new ArgumentException("feature id found"); // The feature does not belong to any project
            }

            return feature;
        }

        public async Task<Feature> UpdateAsync(Feature feature, Guid featureId)
        {
            var existingFeature = await _dbContext.Features
                .FirstOrDefaultAsync(f => f.Id == featureId);

            if (existingFeature == null)
            {
                throw new KeyNotFoundException("Feature not found.");
            }

            // Update properties
            existingFeature.Name = feature.Name;
            existingFeature.Description = feature.Description;
            existingFeature.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return existingFeature;
        }

        //
        public async Task<Feature> UpdateFeatureAsync(Feature featureToUpdate)
        {
            _dbContext.Features.Update(featureToUpdate);
            await _dbContext.SaveChangesAsync();
            return featureToUpdate;
        }

        public async Task<ICollection<Feature>> GetAllAsync(Guid projectId)
        {
            return await _dbContext.Features
                .Where(f => f.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<Feature> DeleteAsync(Guid projectId, Guid featureId)
        {
            var featureToDelete = await _dbContext.Features
                .FirstOrDefaultAsync(f => f.Id == featureId && f.ProjectId == projectId);

            if (featureToDelete == null)
            {
               throw new KeyNotFoundException("Feature not found.");
            }

            _dbContext.Features.Remove(featureToDelete);
            await _dbContext.SaveChangesAsync();
            return featureToDelete;
        }

        
       
    }
}