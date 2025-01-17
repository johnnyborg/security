﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Security.DAL;
using Security.DAL.Entities;
using Security.Requests;
using Security.Security.Model;
using Security.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Security
{
    public class UserLogin
    {
        public const string SESSION_KEY = "user_key";

        private readonly WindesheimDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<User> logger;

        public UserLogin(IHttpContextAccessor httpContextAccessor, WindesheimDbContext dbContext, ILogger<User> logger)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public void Clear()
        {
            httpContextAccessor.HttpContext.Session.Remove(SESSION_KEY);
        }

        public LoginValidationResult ValidateLoginRequest(LoginLoginRequest loginLoginRequest)
        {
            var entity = dbContext.Users.Where(u => u.Email == loginLoginRequest.Email).FirstOrDefault();
            var valid = false;

            if (entity != null)
            {
                valid = entity.Password == User.Hash(loginLoginRequest.Password);

                if (valid)
                {
                    logger.LogInformation($"User login for {loginLoginRequest.Email} accepted");
                }
                else
                {
                    logger.LogInformation($"User login for {loginLoginRequest.Email} denied, password not valid");
                }
            }
            else
            {
                logger.LogInformation($"User login for {loginLoginRequest.Email} denied, no user exists");
            }

            return new LoginValidationResult(valid, entity);
        }

        public void Login(LoginValidationResult validationResult)
        {
            var session = JsonConvert.SerializeObject(new UserStorage(validationResult.User));

            httpContextAccessor.HttpContext.Session.SetString(SESSION_KEY, session);
        }

        public bool HasLogin()
        {
            var storage = GetUser();

            if (storage == null)
                return false;

            if (storage.Entity == null)
                return false;

            return storage.FullyAuthenticated;
        }

        public UserStorage GetUser()
        {
            return UserReader.GetUser(httpContextAccessor.HttpContext);
        }
    }
}
