using Microsoft.AspNetCore.Mvc;
using Library.Persistence.Database;
using Library.Application.Pagination;
using Library.Domain.Data;
using NUnit.Framework;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Structure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class BookController : Controller
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IWebHostEnvironment environment;
        public BookController(UnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            this.environment = environment;
        }
        
        [HttpGet("GetBookList")]
        public List<Book> GetAllBooks()
        {
            return [.. unitOfWork.books.GetBooks()];
        }

        [HttpGet("GetBookListPaginated")]
        public List<Book> GetAllBooksPaginated(int page = 1)
        {
            List<Book> Books = [.. unitOfWork.books.GetBooks()];

            const int pageSize = 5;

            if (page < 1) page = 1;
            int recsCount = Books.Count();
            Pager pager = new Pager(recsCount, page, pageSize);

            int recSkip = (page - 1) * pageSize;
            List<Book> showedData = Books.Skip(recSkip).Take(pager.PageSize).ToList();
            return showedData;
        }

        [HttpGet("GetBookById")]
        public Book GetBookId(Guid id)
        {
            return unitOfWork.books.GetBookById(id);
        }

        [HttpGet("GetBookByName")]
        public Book GetBookName(string Name)
        {
            return unitOfWork.books.GetBookByName(Name);
        }
        
        [HttpPost("AddBook")]
        //[CheckAdmin]
        public async Task<string> AddBook(Book _Book)
        {
            unitOfWork.books.AddBook(_Book);
            await unitOfWork.SaveAsync();

            return "OK";
        }

        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(IFormFile file)
        {
            string fileName;
            if (file != null && file.Length > 0)
            {
                fileName = file.FileName;
                var physPath = environment.ContentRootPath + "/BookCovers/" + file.FileName;
                using (var stream = new FileStream(physPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            else
            {
                fileName = "default.png";
            }
            return fileName;
        }

        [HttpPost("EditBook")]
        public async Task EditBook(Book newBook)
        {
            unitOfWork.books.EditBook(newBook);
            await unitOfWork.SaveAsync();
        }

        [HttpGet("GetBooksForThisUser")]
        public List<Book> GetBooksForThisUser(Guid UserId)
        {
            return unitOfWork.books.GetBooksForThisUser(UserId);
        }

        [HttpDelete("DeleteBook")]
        public async Task DeleteBook(Guid BookId)
        {
            unitOfWork.books.DeleteBook(BookId);
            await unitOfWork.SaveAsync();
        }
    }
}