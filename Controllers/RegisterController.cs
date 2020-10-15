using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Security.DAL;
using Security.Requests;
using Security.Validation;
using Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Controllers
{
    public class RegisterController : Controller
    {
        public readonly WindesheimDbContext myDbContext;

        public RegisterController(WindesheimDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] RegistrationRequest registrationRequest)
        {
            var validator = new RegistrationRequestValidation(registrationRequest);

            if (validator.IsValid())
            {
                var user = new Security.DAL.Entities.User()
                {
                    Name = registrationRequest.Name,
                    Email = registrationRequest.Email,
                    Password = Security.DAL.Entities.User.hash(registrationRequest.Password),
                    Created = DateTime.Now
                };

                myDbContext.Users.Add(user);
                myDbContext.SaveChanges();

                return Redirect("/");
            }

            return View(new RegisterCreateViewModel
            {
                RegistrationRequest = registrationRequest,
                Messages = validator.GetMessages()
            });
        }
    }
}
