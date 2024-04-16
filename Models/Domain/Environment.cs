
namespace ToggleBuddy.API.Models.Domain
{
    public class Environment
    {
        public Guid Id { get; set; }
        public string FeatureID { get; set; }
        public string Name { get; set; }
        public Boolean IsEnabled { get; set; }
        public string Description { get; set; }
        public string KeyValue { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Environment()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}