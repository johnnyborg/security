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
        private RegisterCreateRequest registrationRequest;

        public RegistrationRequestValidation(RegisterCreateRequest registrationRequest)
        {
            this.registrationRequest = registrationRequest;
        }

        protected override void Validate()
        {
            if (this.registrationRequest.Name == "")
                messages.Add("U moet een naam invullen");

            if (this.registrationRequest.Email == "")
                messages.Add("U moet een emailadres invullen");

            var match = Regex.Match(registrationRequest.Email, @"^[^@\s]+@[^@\s\.]+\.[^@\.\s]+$", RegexOptions.IgnoreCase);
            if (!match.Success)
                messages.Add("Uw emailadres lijkt niet op een emailadres");

            if (this.registrationRequest.Password == "" || this.registrationRequest.PasswordValidation == "")
                messages.Add("U moet een wachtwoord invullen");

            if (this.registrationRequest.Password != this.registrationRequest.PasswordValidation)
                messages.Add("De wachtwoorden moeten overeen komen");

            if (this.registrationRequest.Password.Length < 6)
                messages.Add("De minimale wachtwoord lengte is 7 karakters");

            match = Regex.Match(registrationRequest.Password, @"^[\w.]+");
            if (match.Success)
                messages.Add("Het wachtwoord moet minimaal 1 speciaal teken bevatten, hierbij telt een _ niet mee.");
        }
    }
}
