﻿using Security.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Security
{
    public class LoginValidationResult
    {
        public bool Valid { get; private set; }

        public User User { get; private set; }

        public LoginValidationResult(bool valid, User user)
        {
            this.Valid = valid;
            this.User = user;
        }
    }
}