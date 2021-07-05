using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApiJwt.Authorization
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _nextMiddleware;
        private readonly Settings _settings;

        public AuthorizationMiddleware(RequestDelegate nextMiddleware, IOptions<Settings> settings)
        {
            _nextMiddleware = nextMiddleware;
            _settings = settings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthorizationService authorization)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserInfoToContext(context, authorization, token);

            await _nextMiddleware(context);
        }

        private void AttachUserInfoToContext(HttpContext context, IAuthorizationService authorization, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string username = jwtToken.Claims.First(x => x.Type == "username").Value;

                //(...) is a valid user

                // attach to context on successful jwt validation
                context.Items["username"] = username;
            }
            catch
            { 
                // request won't have access to secure routes
            }
        }
    }
}