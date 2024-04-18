
namespace ToggleBuddy.API.Models.Domain
{
    public class FeatureEnvironment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Boolean IsEnabled { get; set; }
        public string Description { get; set; }
        public string KeyValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FeatureId { get; set; }

        //Navigation Properties
        public Feature Feature { get; set; }
    }
}