using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RombiBack.Security.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.JWT
{
    public interface IGenerateToken
    {
        string GenerateToken(int codempresa, int codpais, string user);
    }

    public class GenerateTokenMain : IGenerateToken
    {
        private readonly IConfiguration _configuration;

        public GenerateTokenMain(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int codempresa, int codpais, string user)
        {
            var jwt = _configuration.GetSection("JWT").Get<JwtModel>();
            var claims = new[]
            {
                 new Claim("codempresa", codempresa.ToString()),
                new Claim("codpais", codpais.ToString()),
                new Claim("user", user)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                 issuer: jwt.Issuer,       // Especifica el emisor del token
                audience: jwt.Audience,   // Especifica el destinatario del token
                expires: DateTime.Now.AddHours(4),
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    
    }
}
