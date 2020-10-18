using Security.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Security
{
    public class LoginHelper
    {
        private readonly UserLogin userLogin;

        public LoginHelper (UserLogin userLogin)
        {
            this.userLogin = userLogin;
        }

        public bool HasLogin()
        {
            return userLogin.HasLogin();
        }

        public User GetUser()
        {
            return userLogin.GetUser().Entity;
        }
    }
}
