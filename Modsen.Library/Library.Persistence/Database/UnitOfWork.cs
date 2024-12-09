namespace Library.Persistence.Database;

public sealed class UnitOfWork(LibraryDbContext context, BookRepository bRepos, UserRepository uRepos, AuthorRepository aRepos)
    : IDisposable
{
    private readonly LibraryDbContext context = context;
    private readonly BookRepository bookRepos = bRepos;
    private readonly UserRepository userRepos = uRepos;
    private readonly AuthorRepository authorRepos = aRepos;

    public BookRepository books => bookRepos;
    public UserRepository users => userRepos;
    public AuthorRepository authors => authorRepos;

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    private bool isDisposed;

    private void Dispose(bool disposing)
    {
        if (isDisposed)
        {
            return;
        }

        if (disposing)
        {
            context.Dispose();
        }

        isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}