using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Security.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Middleware
{
    public class NotFullyAuthenticatedMiddleware
    {
        private readonly RequestDelegate next;
        private readonly UserLogin userLogin;

        public NotFullyAuthenticatedMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.userLogin = userLogin;
        }

        public async Task Invoke(HttpContext context)
        {
            var user = UserReader.GetUser(context);

            if (user?.Entity != null)
            {
                var router = context.GetRouteData();

                router.Values.TryGetValue("controller", out object value);
                var controller = (string)value;

                // Check if the user is fully authenticated
                if (user.Entity.AuthenticatorSecret != null && !user.FullyAuthenticated)
                {
                    // When the user not fully authenticated, restrict all other controllers than authentication
                    if (controller != "Authenticator")
                    {
                        context.Response.Redirect("/Authenticator/Validate");
                        return;
                    }
                }
            }

            await next(context);
        }
    }
}
