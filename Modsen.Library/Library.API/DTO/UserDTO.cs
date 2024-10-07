using System.ComponentModel.DataAnnotations;
namespace Library.API.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string ProfilePictureName { get; set; }
    }
}
