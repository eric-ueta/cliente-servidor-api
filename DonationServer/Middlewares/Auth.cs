using DonationServer.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationServer.Middlewares
{
    public class Auth : Attribute, IAsyncAuthorizationFilter
    {
        #region Methods

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Buscar o atributo AllowAnonymous caso exista
            bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

            // Verifica se permite anônimo
            if (!hasAllowAnonymous)
            {
                //Reading the AuthHeader which is signed with JWT
                string token = context.HttpContext.Request.Headers["Token"].ToString()
                    ?? context.HttpContext.Request.Headers["token"].ToString();

                // Verifica se tem um token Caso contrário responde 401
                if (token is null)
                {
                    context.Result = new UnauthorizedObjectResult(new ErrorResponse("Unauthorized.", 401));
                    return;
                }

                if (!AuthManager.LoggedUsers.Any(user => user.Value.Equals(token)))
                {
                    context.Result = new UnauthorizedObjectResult(new ErrorResponse("Unauthorized.", 401));
                }
            }
        }

        #endregion Methods
    }
}