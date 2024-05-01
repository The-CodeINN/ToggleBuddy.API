using System.ComponentModel.DataAnnotations;

namespace ToggleBuddy.API.Models.DTOs.RequestDTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
