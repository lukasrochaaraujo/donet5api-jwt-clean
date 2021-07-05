using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiJw.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    sealed class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext request)
        {
            string tokenHasUsername = request.HttpContext.Items["username"]?.ToString();
            if (tokenHasUsername is null)
                request.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}