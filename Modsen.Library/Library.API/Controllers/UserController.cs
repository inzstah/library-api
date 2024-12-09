using Library.Domain.Data;
using Library.Infrastructure.UseCases.Handlers.UserHandlers;
using Library.Persistence.Database;
using Library.Persistence.JWT;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(
        UserHandler handler)
        : Controller
    {
        [HttpGet("GetUsers")]
        public List<User> GetAllUsers()
        {
            return handler.GetAllUsers();
        }
        [HttpGet("GetUserById")]
        public User GetUserById(Guid id)
        {
            return handler.GetUserById(id);
        }
        [HttpPost("IssueBook")]
        public async Task<string?> IssueBook(Guid bookId, Guid userId)
        {
            return await handler.IssueBook(bookId, userId);
        }
        [HttpPost("ToggleAdmin")]
        public async Task<string> ToggleAdmin(Guid userId)
        {
            return await handler.ToggleAdmin(userId);
        }
        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(string root, IFormFile file)
        {
            return await handler.UploadImage(root, file);
        }
        [HttpPost("JWT_Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            IActionResult response = Unauthorized();
            var token = await handler.Login(email, password);
            if (token != null)
            {
                response = Ok(new { token });
            }
            return response;
        }
        [HttpPost("UpdateUserPfp")]
        public async Task UpdateUserPfp(Guid id, string fileName)
        {
            await handler.UpdateUserPfp(id, fileName);
        }

        [HttpPost("ReturnBook")]//
        public async Task ReturnBook(Guid bookId, Guid userId)
        {
            await handler.ReturnBook(bookId, userId);
        }

    }
}
