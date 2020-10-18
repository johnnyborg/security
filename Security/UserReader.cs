using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Security.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Security
{
    public class UserReader
    {
        private readonly HttpContext httpContext;

        public UserReader(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public static UserStorage GetUser(HttpContext httpContext)
        {
            var reader = new UserReader(httpContext);
            return reader.GetUser();
        }

        public UserStorage GetUser()
        {
            var result = httpContext.Session.GetString(UserLogin.SESSION_KEY);

            if (result == null)
                return null;

            return JsonConvert.DeserializeObject<UserStorage>(result);
        }
    }
}
