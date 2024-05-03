using System.Collections.Generic;
using ToggleBuddy.API.Models.Domain;

public class Feature
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key
    public Guid ProjectId { get; set; }
      

      //Navigation properties
     public virtual ICollection<FeatureEnvironment> FeatureEnvironment { get; set; } = [];
}