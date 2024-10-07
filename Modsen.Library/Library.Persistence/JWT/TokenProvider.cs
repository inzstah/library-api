﻿using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Library.Domain.Data;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.Extensions.Configuration;
namespace Library.Persistence.JWT;

public class TokenProvider(IConfiguration configuration)
{
    private readonly IConfiguration _config = configuration;
    public string Create(User user)
    {
        string secretKey = _config["Jwt:Secret"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("IsAdmin", user.IsAdmin.ToString())
                ]),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);
        return token;
    }
}
