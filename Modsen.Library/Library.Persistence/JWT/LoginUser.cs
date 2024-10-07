using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.Text;
using Library.Domain.Data;
using Library.Persistence.Database;

namespace Library.Persistence.JWT;

public class LoginUser(UnitOfWork uow, TokenProvider tokenProvider)
{
    private readonly UnitOfWork unit = uow;
    private readonly TokenProvider _token = tokenProvider;
    public string Handle(string _Email, string _Password)
    {
        User user = unit.users.GetUserByEmail(_Email) ?? throw new Exception("User not found");
        bool verified = (Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(_Password))).Equals(user.Password, StringComparison.CurrentCultureIgnoreCase));
        if (!verified)
        {
            throw new InvalidOperationException("Incorrect Password");
        }
        string token = _token.Create(user);
        return token;
    }
}
