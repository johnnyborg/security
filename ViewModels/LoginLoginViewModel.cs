using Security.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.ViewModels
{
    public class LoginLoginViewModel
    {
        public IList<string> Messages { get; set; }

        public LoginLoginRequest Request { get; set; }
    }
}
