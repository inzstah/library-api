using System.ComponentModel.DataAnnotations;
namespace Library.Domain.Data
{
    public class User
    {
        [Required]
        public Guid UserId { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string ProfilePictureName { get; set; }
    }
}

