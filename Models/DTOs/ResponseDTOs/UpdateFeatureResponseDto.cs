﻿using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Models.DTOs.ResponseDTOs
{
    public class UpdateFeatureResponseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public Guid ProjectId { get; set; }

        // Navigation properties
        public Project Project { get; set; }
    }
}