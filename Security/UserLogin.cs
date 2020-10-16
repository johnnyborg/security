using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        private readonly WindesheimDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserLogin(WindesheimDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
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

        public void Login(LoginValidationResult validationResult)
        {
            httpContextAccessor.HttpContext.Session.SetString(SESSION_KEY, JsonConvert.SerializeObject(new UserStorage(validationResult.User)));
        }

        public bool HasLogin()
        {
            return httpContextAccessor.HttpContext.Session.GetString(SESSION_KEY) != null;
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
