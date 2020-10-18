using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Requests
{
    public class AuthenticatorValidateCodeRequest
    {
        public string AuthenticatorCode { get; set; }
    }
}
