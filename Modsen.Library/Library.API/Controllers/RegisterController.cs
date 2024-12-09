using System.Security.Cryptography;
using System.Text;
using Library.Domain.Data;
using Library.Persistence.Database;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController(UnitOfWork unitOfWork) : Controller
    {
        private static string Encrypt(string value)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value))).ToLower();
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
