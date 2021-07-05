using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApiJwt.Model;
using WebApiJwt.Models;

namespace WebApiJwt.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly Settings _settings;

        public AuthorizationService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public AuthorizationToken Authenticate(AuthenticationModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.UserName))
                return null;

            //(...) is a valid user
            if (model.UserName == "root" && model.Password == "toor")
                return new AuthorizationToken(GenerateJwtToken(model));

            return null;
        }

        private string GenerateJwtToken(AuthenticationModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.ASCII.GetBytes(_settings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim("username", model.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}