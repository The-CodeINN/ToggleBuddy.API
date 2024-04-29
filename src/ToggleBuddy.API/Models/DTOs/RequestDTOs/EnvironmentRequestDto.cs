using System.ComponentModel.DataAnnotations;

namespace ToggleBuddy.API.Models.DTOs.RequestDTOs
{
    public class EnvironmentRequestDto
    {
        [Required]
        [DataType(DataType.Text)]
        public required string FeatureID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Required]
        public Boolean IsEnabled { get; set; }

        [Required]
        public required string Description { get; set; }


    }
}