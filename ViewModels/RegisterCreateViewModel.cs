using Security.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.ViewModels
{
    public class RegisterCreateViewModel
    {
        public IList<string> Messages { get; set; }

        public RegistrationRequest RegistrationRequest { get; set; }
    }
}
