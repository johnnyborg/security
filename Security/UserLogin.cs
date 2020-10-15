using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using Security.DAL;
using Security.DAL.Entities;
using Security.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Security
{
    public class UserLogin
    {
        const string SESSION_KEY = "user_key";

        private WindesheimDbContext dbContext;

        public UserLogin(WindesheimDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public LoginValidationResult ValidateLoginRequest(LoginLoginRequest loginLoginRequest)
        {
            var entity = dbContext.Users.Where(u => u.Email == loginLoginRequest.Email).FirstOrDefault();
            var valid = false;

            if (entity != null)
            {
                valid = entity.Password == User.Hash(loginLoginRequest.Password);
            }

            return new LoginValidationResult(valid, entity);
        }

        public void Login(HttpContext httpContext, LoginValidationResult validationResult)
        {
            httpContext.Session.SetString(SESSION_KEY, JsonConvert.SerializeObject(new UserStorage(validationResult.User)));
        }

        public UserStorage GetUser(HttpContext httpContext)
        {
            var result = httpContext.Session.GetString(SESSION_KEY);

            if (result == null)
                return null;

            return JsonConvert.DeserializeObject<UserStorage>(result);
        }
    }
}
