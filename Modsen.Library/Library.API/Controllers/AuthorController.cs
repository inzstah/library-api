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
    public class AuthorController : Controller
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IWebHostEnvironment environment;
        public AuthorController(UnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            this.environment = environment;
        }
        
        [HttpGet("GetAuthorList")]
        public List<Author> GetAllAuthors()
        {
            return [.. unitOfWork.authors.GetAuthor()];
        }

        [HttpGet("GetAuthorListPaginated")]
        public List<Author> GetAllAuthorsPaginated(int page = 1)
        {
            List<Author> authors = [.. unitOfWork.authors.GetAuthor()];

            const int pageSize = 5;

            if (page < 1) page = 1;
            int recsCount = authors.Count();
            Pager pager = new Pager(recsCount, page, pageSize);

            int recSkip = (page - 1) * pageSize;
            List<Author> showedData = authors.Skip(recSkip).Take(pager.PageSize).ToList();
            return showedData;
        }

        [HttpGet("GetAuthorById")]
        public Author GetAuthorId(Guid id)
        {
            return unitOfWork.authors.GetAuthorById(id);
        }

        [HttpGet("GetAuthorByName")]
        public Author GetAuthorName(string _FirstName, string _LastName)
        {
            return unitOfWork.authors.GetAuthorByName(_FirstName, _LastName);
        }
        
        [HttpPost("AddAuthor")]
        //[CheckAdmin]
        public async Task<string> AddAuthor(Author _Author)
        {
            unitOfWork.authors.AddAuthor(_Author);
            await unitOfWork.SaveAsync();
            return "OK";
        }


        [HttpPost("EditAuthor")]
        public async Task EditAuthor(Author newAuthor)
        {
            unitOfWork.authors.EditAuthor(newAuthor);
            await unitOfWork.SaveAsync();
        }

        [HttpDelete("DeleteAuthor")]
        public async Task DeleteAuthor(Guid AuthorId)
        {
            unitOfWork.authors.DeleteAuthor(AuthorId);
            await unitOfWork.SaveAsync();
        }
    }
}