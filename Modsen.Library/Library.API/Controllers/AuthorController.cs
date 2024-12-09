using Library.Domain.Data;
using Library.Infrastructure.UseCases.Handlers.AuthorHandlers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("[controller]")]
   //[Authorize]
    public class AuthorController(AuthorHandler handler) : Controller
    {
        [HttpGet("GetAuthorList")]
        public List<Author> GetAllAuthors()
        {
            return handler.GetAllAuthors();
        }

        [HttpGet("GetAuthorListPaginated")]
        public List<Author> GetAllAuthorsPaginated(int page = 1)
        {
            return handler.GetAllAuthorsPaginated(page);
        }

        [HttpGet("GetAuthorById")]
        public Author GetAuthorId(Guid id)
        {
            return handler.GetAuthorId(id);
        }

        [HttpGet("GetAuthorByName")]
        public Author GetAuthorName(string firstName, string lastName)
        {
            return handler.GetAuthorName(firstName, lastName);
        }
        
        [HttpPost("AddAuthor")]
        //[CheckAdmin]
        public async Task<string> AddAuthor(Author author)
        {
            
            return await handler.AddAuthor(author);;
        }


        [HttpPost("UpdateAuthor")]
        public async Task UpdateAuthor(Author newAuthor)
        {
            await handler.UpdateAuthor(newAuthor);
        }

        [HttpDelete("DeleteAuthor")]
        public async Task DeleteAuthor(Guid authorId)
        {
            await handler.DeleteAuthor(authorId);
        }
    }
}