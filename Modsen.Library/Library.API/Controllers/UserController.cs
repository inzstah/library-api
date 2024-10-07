using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Data;
using Library.Persistence.Database;
using Library.Persistence.JWT;

namespace EventList.API.Structure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly TokenProvider tokenProvider;
        private readonly LoginUser loginUser;
        private readonly IWebHostEnvironment environment;
        public UserController(UnitOfWork unitOfWork, TokenProvider tokenProvider, LoginUser loginUser, IWebHostEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            this.tokenProvider = tokenProvider;
            this.loginUser = loginUser;
            this.environment = environment;
        }


        [HttpGet("GetUsers")]
        public List<User> GetAllUsers()
        {
            return unitOfWork.users.GetUsers().ToList();
        }
        [HttpGet("GetUserById")]
        public User GetUserById(Guid id)
        {
            return unitOfWork.users.GetUserById(id);
        }
        [HttpPost("IssueBook")]
        public async Task<string> IssueBook(Guid BookId, Guid UserId)
        {
            try{
                unitOfWork.users.IssueBook(BookId, UserId);
                await unitOfWork.SaveAsync();
                return "Added";
            }
            catch(Exception e)
            {
                return e.InnerException?.Message;
            }
        }
        [HttpPost("ToggleAdmin")]
        public async Task<string> ToggleAdmin(Guid UserId)
        {
            unitOfWork.users.ToggleAdmin(UserId);
            await unitOfWork.SaveAsync();
            return "Promoted user to admin";
        }
        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(IFormFile file)
        {
            string fileName;
            if (file != null && file.Length > 0)
            {
                fileName = file.FileName;
                var physPath = environment.ContentRootPath + "/UserPhotos/" + file.FileName;
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
        [HttpPost("JWT_Login")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            IActionResult response = Unauthorized();
            var token = loginUser.Handle(Email, Password);
            if (token != null)
            {
                response = Ok(new { token });
            }
            return response;
        }
        [HttpPost("UpdateUserPfp")]
        public async Task UpdateUserPfp(Guid id, string fileName)
        {
            unitOfWork.users.UpdateUserPfp(id, fileName);
            await unitOfWork.SaveAsync();
        }

        [HttpPost("ReturnBook")]//
        public async Task ReturnBook(Guid BookId, Guid UserId)
        {
            unitOfWork.users.ReturnBook(BookId, UserId);
            await unitOfWork.SaveAsync();
        }

    }
}
