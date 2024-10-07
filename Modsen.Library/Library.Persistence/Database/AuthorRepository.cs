using Library.Domain.Data;
namespace Library.Persistence.Database
{
    public class AuthorRepoisitory(LibraryDbContext context)
    {
        private readonly LibraryDbContext context = context;

        public IQueryable<Author> GetAuthor()
        {
            return context.authors;
        }
        
        public Author GetAuthorById(Guid _id)
        {
            return context.authors.Single(x => x.AuthorId == _id);
        }
       
        public Author GetAuthorByName(string _FirstName, string _LastName)
        {
            //private string authorName = _FirstName + " " + _LastName;
            return context.authors.Single(x => x.FirstName == _FirstName && x.LastName == _LastName);
        }
       
        public void AddAuthor(Author _entity)
        {
            if (context.authors.All(e => e.AuthorId != _entity.AuthorId)) 
            {
                context.authors.Add(_entity);
            }
        }
        
        public void EditAuthor(Author _newAuthor)
        {
            context.authors.Entry(GetAuthorById(_newAuthor.AuthorId)).CurrentValues.SetValues(_newAuthor);
        }
        
        public void DeleteAuthor(Guid _AuthorId)
        {
            if (context.authors.Any(e => e.AuthorId == _AuthorId))
            {
                context.authors.Remove(GetAuthorById(_AuthorId));
            }
        }
    }
}
