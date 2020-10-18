using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Security.Requests;
using Security.Security;
using Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Controllers
{
    public class AuthenticatorController : Controller
    {
        private readonly UserAuthenticator userAuthenticator;
        private readonly UserLogin userLogin;

        public AuthenticatorController(UserAuthenticator userAuthenticator, UserLogin userLogin)
        {
            this.userAuthenticator = userAuthenticator;
            this.userLogin = userLogin;
        }

        public IActionResult Index()
        {
            if (!userLogin.HasLogin())
                throw new Exception("access prohibited");

            return View(new AuthenticatorIndexViewModel()
            {
                QRCode = userAuthenticator.GetQRCode(),
            });
        }

        public IActionResult Activate()
        {
            if (!userLogin.HasLogin())
                throw new Exception("access prohibited");

            userAuthenticator.ActivateAuthenticator();
            return Redirect("/");
        }

        public IActionResult Validate()
        {
            return View();
        }

        public IActionResult ValidateCode([FromForm] AuthenticatorValidateCodeRequest authenticatorValidateCodeRequest)
        {
            if (userAuthenticator.ValidateLoginCode(authenticatorValidateCodeRequest.AuthenticatorCode))
                return Redirect("/");

            return View();
        }

        public IActionResult Complete()
        {
            return View();
        }
    }
}
