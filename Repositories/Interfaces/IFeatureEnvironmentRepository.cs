using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Repositories.Interfaces
{
    public interface IFeatureEnvironmentRepository
    {
        Task<FeatureEnvironment> CreateFeatureEnvironmentAsync(Guid featureId, FeatureEnvironment featureEnvironment);
        Task<List<FeatureEnvironment>> GetAllFeatureEnvironmentsAsync(Guid featureId);
        Task<FeatureEnvironment?> UpdateFeatureEnvironmentStatusAsync(Guid featureId, Guid featureEnvironmentId, FeatureEnvironment featureEnvironment);
        Task<FeatureEnvironment?> DeleteFeatureEnvironmentAsync(Guid featureEnvironmentId);

        // filters single FeatureEnvironment
        Task<FeatureEnvironment?> GetFeatureEnvironmentByIdForCurrentFeatureAsync(Guid featureEnvironmentId, Guid featureId);
    }
}