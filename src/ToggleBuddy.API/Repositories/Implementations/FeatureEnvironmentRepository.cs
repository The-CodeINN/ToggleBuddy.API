using Microsoft.EntityFrameworkCore;
using ToggleBuddy.API.Data;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Repositories.Interfaces;


namespace ToggleBuddy.API.Repositories.Implementations
{
    public class FeatureEnvironmentRepository : IFeatureEnvironmentRepository
    {
        private readonly ToggleBuddyDbContext _dbContext;

        public FeatureEnvironmentRepository(ToggleBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FeatureEnvironment> CreateFeatureEnvironmentAsync(FeatureEnvironment featureEnvironment, Guid featureId)
        {
            featureEnvironment.FeatureId = featureId;
            await _dbContext.FeatureEnvironments.AddAsync(featureEnvironment);
            await _dbContext.SaveChangesAsync();

            return featureEnvironment;
        }

        public async Task<List<FeatureEnvironment>> GetAllFeatureEnvironmentsAsync(Guid featureId)
        {
            return await _dbContext.FeatureEnvironments
                .Where(e => e.FeatureId == featureId)
                .ToListAsync();
        }

        public async Task<FeatureEnvironment> UpdateFeatureEnvironmentStatusAsync(Guid featureId, Guid featureEnvironmentId, FeatureEnvironment featureEnvironment)
        {
            var existingFeatureEnvironment = await _dbContext.FeatureEnvironments.FirstOrDefaultAsync(e => e.Id == featureId && e.Id == featureEnvironmentId);
            if (existingFeatureEnvironment == null)
            {
                throw new KeyNotFoundException("Environment not found.");
            }

            //insert implementation for checking if feature is enabled or disabled
            existingFeatureEnvironment.IsEnabled = featureEnvironment.IsEnabled;
            existingFeatureEnvironment.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return existingFeatureEnvironment;
        }

        public async Task<FeatureEnvironment?> DeleteFeatureEnvironmentAsync(Guid featureId, Guid featureEnvironmentId)
        {
            var existingFeatureEnvironment = await _dbContext.FeatureEnvironments.FirstOrDefaultAsync(e => e.Id == featureId && e.Id == featureEnvironmentId);
            if (existingFeatureEnvironment == null)
            {
                return null;
            }

            _dbContext.FeatureEnvironments.Remove(existingFeatureEnvironment);
            await _dbContext.SaveChangesAsync();

            return existingFeatureEnvironment;
        }

        public async Task<FeatureEnvironment?> GetFeatureEnvironmentByIdForCurrentFeatureAsync(Guid featureEnvironmentId, Guid featureId)
        {
            var environment = await _dbContext.FeatureEnvironments.FindAsync(featureEnvironmentId);
            if (environment?.FeatureId != featureId)
                return null; // If the environment doesn't exist under the current feature, return null
            return environment;
        }
    }
}