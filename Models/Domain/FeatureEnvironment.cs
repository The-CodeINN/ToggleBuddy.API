
namespace ToggleBuddy.API.Models.Domain
{
    public class FeatureEnvironment
    {
        public Guid Id { get; set; }
        public Guid FeatureId { get; set; }
        public string Name { get; set; } = null!;
        public Boolean IsEnabled { get; set; }
        public string Description { get; set; } = null!;
        public string KeyValue { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        // Setting non-nullable properties in the constructor
        // public FeatureEnvironment(string name, string description, string keyValue, bool isEnabled, string featureId)
        // {
        //     Id = Guid.NewGuid();
        //     Name = name;
        //     Description = description;
        //     KeyValue = keyValue;
        //     IsEnabled = isEnabled;
        //     FeatureId = featureId;
        // }
    }
}