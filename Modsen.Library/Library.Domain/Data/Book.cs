using System.ComponentModel.DataAnnotations;
namespace Library.Domain.Data
{
    public class Book
    {
        public required Guid BookId { get; set; }
        public required int Isbn { get; set; }
        public required string BookName { get; set; }
        public required string BookGenre { get; set; }
        public required string BookDescription { get; set; }
        public required Guid AuthorId { get; set; }
        public DateOnly? TimeTaken { get; set; }
        public DateOnly TimeToReturn { get; set; }       
        public string BookCoverName { get; set; }
    }
}
