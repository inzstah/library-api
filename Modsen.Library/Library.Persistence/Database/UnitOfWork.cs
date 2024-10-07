namespace Library.Persistence.Database;

public class UnitOfWork(LibraryDbContext context, BookRepository bRepos, UserRepository uRepos, AuthorRepoisitory aRepos) : IDisposable
{

    private readonly LibraryDbContext context = context;
    private readonly BookRepository bookRepos = bRepos;
    private readonly UserRepository userRepos = uRepos;
    private readonly AuthorRepoisitory authorRepos = aRepos;

    public BookRepository books
    {
        get
        {
            return bookRepos;
        }
    }
    public UserRepository users
    {
        get
        {
            return userRepos;
        }
    }
    public AuthorRepoisitory authors
    {
        get
        {
            return authorRepos;
        }
    }
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
    private bool isDisposed = false;
    public virtual void Dispose(bool disposing)
    {
        if (!this.isDisposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
            this.isDisposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
