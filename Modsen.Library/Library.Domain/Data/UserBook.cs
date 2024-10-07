namespace Library.Domain.Data
{
    public class UserBook
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public UserBook(Guid UserId, Guid BookId)
        {
            this.UserId = UserId;
            this.BookId = BookId;
        }
    }
}