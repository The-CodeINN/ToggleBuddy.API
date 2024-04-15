using System.ComponentModel.DataAnnotations;

namespace ToggleBuddy.API.Models.DTOs.RequestDTOs
{
    public class ProjectRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot be more than 50 characters"),
        MinLength(3, ErrorMessage = "Name cannot be less than 3 characters")]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
