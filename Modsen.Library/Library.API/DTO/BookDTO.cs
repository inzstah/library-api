using System.ComponentModel.DataAnnotations;
namespace Library.API.DTO
{
    public class BookDTO
    {
        public Guid BookId { get; set; }
        public int Isbn { get; set; }
        public string BookName { get; set; }
        public string BookGenre { get; set; }
        public string BookDescription { get; set; }
        public Guid AuthorId { get; set; }
        public DateOnly? TimeTaken { get; set; }
        public DateOnly TimeToReturn { get; set; }       
        public string BookCoverName { get; set; }
    }
}
