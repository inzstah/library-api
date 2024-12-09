using Library.Application.Pagination;
using Library.Domain.Data;
using Library.Persistence.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Infrastructure.UseCases.Handlers.BookHandlers;

public class BookHandler(UnitOfWork unitOfWork)
{
    public List<Book> GetAllBooks()
    {
        return [.. unitOfWork.books.GetBooks()];
    }
    
    public List<Book> GetAllBooksPaginated(int page = 1)
    {
        List<Book> Books = [.. unitOfWork.books.GetBooks()];

        const int pageSize = 5;

        if (page < 1) page = 1;
        var recsCount = Books.Count;
        var pager = new Pager(recsCount, page, pageSize);

        var recSkip = (page - 1) * pageSize;
        var showedData = Books.Skip(recSkip).Take(pager.PageSize).ToList();
        return showedData;
    }
    
    public Book GetBookId(Guid id)
    {
        return unitOfWork.books.GetBookById(id);
    }
    
    public Book GetBookName(string name)
    {
        return unitOfWork.books.GetBookByName(name);
    }
    
    //[CheckAdmin]
    public async Task<string> AddBook(Book book)
    {
        unitOfWork.books.AddBook(book);
        await unitOfWork.SaveAsync();

        return "OK";
    }
    
    public async Task<string> UploadImage(string root, IFormFile? file)
    {
        string fileName;
        if (file is { Length: > 0 })
        {
            fileName = file.FileName;
            var physPath = root + "/BookCovers/" + file.FileName;
            await using var stream = new FileStream(physPath, FileMode.Create);
            await file.CopyToAsync(stream);
        }
        else
        {
            fileName = "default.png";
        }

        return fileName;
    }
    
    public async Task UpdateBook(Book newBook)
    {
        unitOfWork.books.UpdateBook(newBook);
        await unitOfWork.SaveAsync();
    }
    
    public List<Book> GetBooksForThisUser(Guid userId)
    {
        return unitOfWork.books.GetBooksForThisUser(userId);
    }
    
    public async Task DeleteBook(Guid bookId)
    {
        unitOfWork.books.DeleteBook(bookId);
        await unitOfWork.SaveAsync();
    }
}