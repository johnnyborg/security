using Security.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Validation
{
    public class LoginLoginRequestValidation : AbstractValidation
    {
        private LoginLoginRequest loginRequest;

        public LoginLoginRequestValidation(LoginLoginRequest loginRequest)
        {
            this.loginRequest = loginRequest;
        }

        protected override void Validate()
        {
            if (loginRequest.Email == "")
                messages.Add("Email is verplicht");

            if (loginRequest.Password == "")
                messages.Add("Wachtwoord is verplicht");
        }
    }
}
