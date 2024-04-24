using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Repositories.Interfaces
{
    public interface IFeatureEnvironmentRepository
    {
        Task<FeatureEnvironment> CreateFeatureEnvironmentAsync(FeatureEnvironment featureEnvironment);
        Task<List<FeatureEnvironment>> GetFeatureEnvironmentAsync();
        Task<FeatureEnvironment?> UpdateFeatureEnvironmentStatusAsync(Guid id, FeatureEnvironment featureEnvironment);
        Task<FeatureEnvironment?> DeleteFeatureEnvironmentAsync(Guid id);

        // filters single FeatureEnvironment
        Task<FeatureEnvironment?> GetFeatureEnvironmentByIdForCurrentFeatureAsync(Guid id, string? featureId);
    }
}