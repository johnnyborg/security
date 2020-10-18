using Google.Authenticator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Security.DAL;
using Security.DAL.Entities;
using Security.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Security
{
    public class UserAuthenticator
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly WindesheimDbContext dbContext;
        private readonly UserLogin userLogin;
        private readonly IOptions<Settings> options;

        public UserAuthenticator(IHttpContextAccessor httpContextAccessor, WindesheimDbContext dbContext, UserLogin userLogin, IOptions<Settings> options)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userLogin = userLogin;
            this.options = options;
            this.dbContext = dbContext;
        }

        public void ActivateAuthenticator()
        {
            var login = userLogin.GetUser();

            if (login == null)
                throw new Exception("This method can only be called when the user has loggedin");

            var user = dbContext.Users.FirstOrDefault(u => u.Id == login.Entity.Id);

            if (user == null)
                throw new Exception("This user does not exist anymore, did you get in an argument?");

            user.AuthenticatorSecret = QRSecret(user);

            dbContext.Update(user);
            dbContext.SaveChanges();

            var session = JsonConvert.SerializeObject(new UserStorage(user, true));
            httpContextAccessor.HttpContext.Session.SetString(UserLogin.SESSION_KEY, session);
        }

        public string GetQRCode()
        {
            var login = userLogin.GetUser();

            if (login?.Entity == null)
                throw new Exception("User must be loggedin");

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("Windesheim security", login.Entity.Email, QRSecret(login.Entity), false, 300);

            return setupInfo.QrCodeSetupImageUrl;
        }

        public bool ValidateLoginCode(string code)
        {
            var user = userLogin.GetUser();

            if (user?.Entity == null)
                throw new Exception("User cannot be null at this moment");

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            bool isCorrectPIN = tfa.ValidateTwoFactorPIN(user.Entity.AuthenticatorSecret, code);

            if (isCorrectPIN)
            {
                var session = JsonConvert.SerializeObject(new UserStorage(user.Entity, true));
                httpContextAccessor.HttpContext.Session.SetString(UserLogin.SESSION_KEY, session);
            }

            return isCorrectPIN;
        }

        private string QRSecret(User user)
        {
            return User.Hash($"{options.Value.SharedSecret}__{user.Email}__{user.Created.ToString()}");
        }
    }
}
