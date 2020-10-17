using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Security.DAL;
using Security.DAL.Entities;
using Security.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Security
{
    public class UserRegistration
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly WindesheimDbContext dbContext;
        private readonly ILogger<User> logger;

        public UserRegistration(IHttpContextAccessor httpContextAccessor, WindesheimDbContext dbContext, ILogger<User> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public void Register(RegisterCreateRequest registerCreateRequest)
        {
            var user = new User()
            {
                Name = registerCreateRequest.Name,
                Email = registerCreateRequest.Email,
                Password = User.Hash(registerCreateRequest.Password),
                Created = DateTime.Now
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            logger.LogInformation($"Created new account for {registerCreateRequest.Email} by {httpContextAccessor.HttpContext.Connection.RemoteIpAddress}");
        }
    }
}
