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
        public readonly WindesheimDbContext dbContext;

        public RegisterController(WindesheimDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] RegisterCreateRequest registerCreateRequest)
        {
            var validator = new RegistrationRequestValidation(registerCreateRequest);

            if (validator.IsValid())
            {
                var user = new DAL.Entities.User()
                {
                    Name = registerCreateRequest.Name,
                    Email = registerCreateRequest.Email,
                    Password = DAL.Entities.User.Hash(registerCreateRequest.Password),
                    Created = DateTime.Now
                };

                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                return Redirect("/");
            }

            return View(new RegisterCreateViewModel
            {
                RegisterCreateRequest = registerCreateRequest,
                Messages = validator.GetMessages()
            });
        }
    }
}
