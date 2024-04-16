using System.ComponentModel.DataAnnotations;

namespace ToggleBuddy.API.Models.DTOs.RequestDTOs
{
    public class FeatureRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }



    }
}
