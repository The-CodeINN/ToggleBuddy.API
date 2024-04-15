using System.ComponentModel.DataAnnotations;

namespace ToggleBuddy.API.Models.DTOs.RequestDTOs
{
    public class ProjectRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot be more than 50 characters"),
        MinLength(3, ErrorMessage = "Name cannot be less than 3 characters")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
