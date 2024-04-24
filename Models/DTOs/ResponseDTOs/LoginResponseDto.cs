namespace ToggleBuddy.API.Models.DTOs.ResponseDTOs
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
