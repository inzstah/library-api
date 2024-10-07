using System.ComponentModel.DataAnnotations;
namespace Library.Domain.Data
{
    public class Book
    {
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public int Isbn { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string BookGenre { get; set; }
        [Required]
        public string BookDescription { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public DateOnly? TimeTaken { get; set; }
        public DateOnly TimeToReturn { get; set; }       
        public string BookCoverName { get; set; }
    }
}
