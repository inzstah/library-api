using Library.Domain.Data;
using Library.Infrastructure.UseCases.Handlers.BookHandlers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class BookController(BookHandler handler, IWebHostEnvironment environment) : Controller
    {
        [HttpGet("GetBookList")]
        public List<Book> GetAllBooks()
        {
            return handler.GetAllBooks();
        }

        [HttpGet("GetBookListPaginated")]
        public List<Book> GetAllBooksPaginated(int page = 1)
        {
            return handler.GetAllBooksPaginated(page);
        }

        [HttpGet("GetBookById")]
        public Book GetBookId(Guid id)
        {
            return handler.GetBookId(id);
        }

        [HttpGet("GetBookByName")]
        public Book GetBookName(string name)
        {
            return handler.GetBookName(name);
        }

        [HttpPost("AddBook")]
        //[CheckAdmin]
        public async Task<string> AddBook(Book book)
        {
            await handler.AddBook(book);
            return "OK";
        }

        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(string root, IFormFile file)
        {
            return await handler.UploadImage(root, file);
        }

        [HttpPost("UpdateBook")]
        public async Task UpdateBook(Book newBook)
        {
            await handler.UpdateBook(newBook);
        }

        [HttpGet("GetBooksForThisUser")]
        public List<Book> GetBooksForThisUser(Guid userId)
        {
            return handler.GetBooksForThisUser(userId);
        }

        [HttpDelete("DeleteBook")]
        public async Task DeleteBook(Guid bookId)
        {
            await handler.DeleteBook(bookId);
        }
    }
}