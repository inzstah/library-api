using System.ComponentModel.DataAnnotations;
namespace Library.Domain.Data
{
    public class Author
    {
        public required Guid AuthorId { get; set; }
        public required string FirstName {get; set; }
        public required string LastName {get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Country { get; set; }
    }
}