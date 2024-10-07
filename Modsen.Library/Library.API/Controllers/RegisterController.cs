using Library.Domain.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Library.Persistence.Database;

namespace Library.API.Structure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        public RegisterController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private string Encrypt(string value)
        {
            var hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(value))).ToLower();
        }
        [HttpPost("RegisterUser")]
        
        public async Task<string> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Encrypt(user.Password);
                unitOfWork.users.AddUser(user);
                await unitOfWork.SaveAsync();
                return "200 OK\n" +
                    $"{user.Password}";
            } else
            {
                return "Failed";
            }
        }
    }
}
