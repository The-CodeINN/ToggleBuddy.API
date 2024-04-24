using Microsoft.EntityFrameworkCore;
using ToggleBuddy.API.Data;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Repositories.Interfaces;


namespace ToggleBuddy.API.Repositories.Implementations
{
    public class FeatureEnvironmentRepository : IFeatureEnvironment
    {
        private readonly ToggleBuddyDbContext dbContext;

        public FeatureEnvironmentRepository(ToggleBuddyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<FeatureEnvironment> CreateFeatureEnvironmentAsync(FeatureEnvironment featureEnvironment)
        {
            await dbContext.FeatureEnvironments.AddAsync(featureEnvironment);
            await dbContext.SaveChangesAsync();

            return featureEnvironment;
        }

        public async Task<List<FeatureEnvironment>> GetFeatureEnvironmentAsync()
        {
            return await dbContext.FeatureEnvironments.ToListAsync();
        }

        public async Task<FeatureEnvironment?> UpdateFeatureEnvironmentStatusAsync(Guid id, FeatureEnvironment featureEnvironment)
        {
            var existingFeatureEnvironment = await dbContext.FeatureEnvironments.FirstOrDefaultAsync(p => p.Id == id);
            if (existingFeatureEnvironment == null)
            {
                return null;
            }

            //insert implementation for checking if feature is enbled or disabled


            await dbContext.SaveChangesAsync();
            return existingFeatureEnvironment;
        }

        public async Task<FeatureEnvironment?> DeleteFeatureEnvironmentAsync(Guid id)
        {
            var existingFeatureEnvironment = await dbContext.FeatureEnvironments.FirstOrDefaultAsync(p => p.Id == id);
            if (existingFeatureEnvironment == null)
            {
                return null;
            }

            dbContext.FeatureEnvironments.Remove(existingFeatureEnvironment);
            await dbContext.SaveChangesAsync();

            return existingFeatureEnvironment;
        }

        public async Task<FeatureEnvironment?> GetFeatureEnvironmentByIdForCurrentFeatureAsync(Guid id, string? featureId)
        {
            var environment = await dbContext.FeatureEnvironments.FindAsync(id);
            if (environment?.FeatureId != featureId)
                return null; // If the environment doesn't exist under the current feature, return null
            return environment;
        }
    }
}