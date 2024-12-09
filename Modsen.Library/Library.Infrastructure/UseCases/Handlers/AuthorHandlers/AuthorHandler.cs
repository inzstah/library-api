using Library.Application.Pagination;
using Library.Domain.Data;
using Library.Persistence.Database;

namespace Library.Infrastructure.UseCases.Handlers.AuthorHandlers;

public class AuthorHandler(UnitOfWork unitOfWork)
{
    public List<Author> GetAllAuthors()
    {
        return [.. unitOfWork.authors.GetAuthor()];
    }
    
    public List<Author> GetAllAuthorsPaginated(int page = 1)
    {
        List<Author> authors = [.. unitOfWork.authors.GetAuthor()];

        const int pageSize = 5;

        if (page < 1) page = 1;
        var recsCount = authors.Count;
        var pager = new Pager(recsCount, page, pageSize);

        var recSkip = (page - 1) * pageSize;
        var showedData = authors.Skip(recSkip).Take(pager.PageSize).ToList();
        return showedData;
    }
    
    public Author GetAuthorId(Guid id)
    {
        return unitOfWork.authors.GetAuthorById(id);
    }
    
    public Author GetAuthorName(string firstName, string lastName)
    {
        return unitOfWork.authors.GetAuthorByName(firstName, lastName);
    }
    
    //[CheckAdmin]
    public async Task<string> AddAuthor(Author author)
    {
        unitOfWork.authors.AddAuthor(author);
        await unitOfWork.SaveAsync();
        return "OK";
    }

    
    public async Task UpdateAuthor(Author newAuthor)
    {
        unitOfWork.authors.UpdateAuthor(newAuthor);
        await unitOfWork.SaveAsync();
    }
    
    public async Task DeleteAuthor(Guid authorId)
    {
        unitOfWork.authors.DeleteAuthor(authorId);
        await unitOfWork.SaveAsync();
    }
}