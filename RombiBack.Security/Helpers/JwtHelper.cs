using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RombiBack.Security.Model;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace RombiBack.Security.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string dni)
        {
            var jwt = _configuration.GetSection("JWT").Get<JwtModel>();
            var claims = new[]
            {
                new Claim("dni", dni)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetClaimValue(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);

            return claim?.Value;
        }
        //private readonly IConfiguration _configuration;

        //public JwtHelper(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //public string GenerateToken(string dni)
        //{
        //    var jwt = _configuration.GetSection("JWT").Get<JwtModel>();
        //    var claims = new[]
        //    {
        //    new Claim("dni", dni)
        //};
        //    var key = GenerateSymmetricKey(jwt.KeyLength); // Use the method to generate a key
        //    var signIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(10),
        //        signingCredentials: signIn
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private SymmetricSecurityKey GenerateSymmetricKey(int keySize)
        //{
        //    Console.WriteLine($"Requested key size: {keySize} bits");

        //    if (keySize < 256)
        //    {
        //        throw new ArgumentException("Key size must be at least 256 bits.", nameof(keySize));
        //    }

        //    var keyBytes = new byte[keySize / 8];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(keyBytes);
        //    }

        //    return new SymmetricSecurityKey(keyBytes);
        //}
    }
}
