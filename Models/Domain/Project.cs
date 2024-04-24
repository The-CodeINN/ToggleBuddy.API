namespace ToggleBuddy.API.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }

        // Navigation properties
        public List<Feature> Features { get; set; } = []; // Initialize to avoid null reference issues
    }
}