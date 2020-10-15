using Microsoft.AspNetCore.Server.IIS.Core;
using Security.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Security.Validation
{
    public class RegistrationRequestValidation : AbstractValidation
    {
        private RegistrationRequest registrationRequest;

        public RegistrationRequestValidation(RegistrationRequest registrationRequest)
        {
            this.registrationRequest = registrationRequest;
        }

        protected override void Validate()
        {
            if (this.registrationRequest.Name == "")
                messages.Add("U moet een naam invullen");

            if (this.registrationRequest.Email == "")
                messages.Add("U moet een emailadres invullen");

            if (this.registrationRequest.Password == "" || this.registrationRequest.PasswordValidation == "")
                messages.Add("U moet een wachtwoord invullen");

            if (this.registrationRequest.Password != this.registrationRequest.PasswordValidation)
                messages.Add("De wachtwoorden moeten overeen komen");

            var match = Regex.Match(registrationRequest.Email, @"^[^@\s]+@[^@\s\.]+\.[^@\.\s]+$", RegexOptions.IgnoreCase);
            if (!match.Success)
                messages.Add("Uw emailadres lijkt niet op een emailadres");
        }
    }
}
