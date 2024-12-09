using Library.Domain.Data;
namespace Library.Persistence.Database
{
    public class BookRepository(LibraryDbContext context)
    {
        private readonly LibraryDbContext context = context;
        
        public IQueryable<Book> GetBooks()
        {
            return context.books;
        }
        
        public Book GetBookById(Guid _id)
        {
            return context.books.Single(x => x.BookId == _id);
        }
       
        public Book GetBookByName(string _name)
        {
            return context.books.Single(x => x.BookName == _name);
        }
       
        public void AddBook(Book _entity)
        {
            if (context.books.All(e => e.BookId != _entity.BookId)) 
            {
                context.books.Add(_entity);
            }
        }
        
        public void UpdateBook(Book _newBook)
        {
            context.books.Entry(GetBookById(_newBook.BookId)).CurrentValues.SetValues(_newBook);
        }
        
        public void DeleteBook(Guid _BookId)
        {
            if (context.books.Any(e => e.BookId == _BookId))
            {
                context.books.Remove(GetBookById(_BookId));
            }
        }
        public List<Book> GetBooksForThisUser(Guid _UserId)
        {
            List<UserBook> ub = [.. context.userBooks.Where(ub => ub.UserId == _UserId)];
            List<Book> Books = new List<Book>();
            foreach (UserBook userBook in ub)
            {
                Books.Add(GetBookById(userBook.BookId));
            }
            return Books;
        }
    }
}
