using Google.Authenticator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Security.DAL;
using Security.Requests;
using Security.Security;
using Security.Validation;
using Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserLogin userLogin;

        public LoginController(UserLogin userLogin)
        {
            this.userLogin = userLogin;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginLoginRequest loginRequest)
        {
            var validator = new LoginLoginRequestValidation(loginRequest);
            var result = userLogin.ValidateLoginRequest(loginRequest);

            if (validator.IsValid() && result.Valid)
            {
                userLogin.Login(result);

                if (result.RequiresAuthenticator)
                    return Redirect("/Authenticator/Validate");

                return Redirect("/");
            }

            var viewModel = new LoginLoginViewModel()
            {
                Messages = validator.GetMessages()
            };

            if (!result.Valid && result.User == null)
            {
                viewModel.Messages.Add("Dit emailadres bestaat niet");
            }
            else if (!result.Valid)
            {
                viewModel.Messages.Add("De combinatie van de gebruikersnaam en/of wachtwoord is onjuist");
            }

            return View(viewModel);
        }

        public IActionResult Logout()
        {
            userLogin.Clear();
            return Redirect("/");
        }
    }
}
