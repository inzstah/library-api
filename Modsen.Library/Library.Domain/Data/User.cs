using System.ComponentModel.DataAnnotations;
namespace Library.Domain.Data
{
    public class User
    {
        public required Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string ProfilePictureName { get; set; }
    }
}

