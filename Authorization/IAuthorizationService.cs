using WebApiJwt.Model;
using WebApiJwt.Models;

namespace WebApiJwt.Authorization
{
    public interface IAuthorizationService
    {
        AuthorizationToken Authenticate(AuthenticationModel model);
    }
}