using System.ComponentModel.DataAnnotations;
namespace Library.Domain.Data
{
    public class Author
    {
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string FirstName {get; set; }
        [Required]
        public string LastName {get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string Country { get; set; }
    }
}