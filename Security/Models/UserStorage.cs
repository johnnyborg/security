using Security.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Security.Models
{
    public class UserStorage
    {
        public User Entity { get; set; }

        public bool FullyAuthenticated { get; set; }

        public UserStorage()
        {

        }

        public UserStorage(User user)
        {
            Entity = user;
            FullyAuthenticated = user.AuthenticatorSecret == null;
        }

        public UserStorage(User user, bool fullyAuthenticated)
        {
            Entity = user;
            FullyAuthenticated = fullyAuthenticated;
        }
    }
}
