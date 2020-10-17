using Microsoft.AspNetCore.Mvc;
using Security.Requests;
using Security.Security;
using Security.Validation;
using Security.ViewModels;

namespace Security.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserRegistration userRegistration;

        public RegisterController(UserRegistration userRegistration)
        {
            this.userRegistration = userRegistration;
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
                userRegistration.Register(registerCreateRequest);
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
