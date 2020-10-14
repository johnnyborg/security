using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Validation
{
    public abstract class AbstractValidation
    {
        private bool runValidation = true;

        protected List<string> messages { get; set; } = new List<string>();

        protected abstract void Validate();

        public virtual bool IsValid()
        {
            RunValidation();
            return messages.Count() == 0;
        }

        public IList<string> GetMessages()
        {
            RunValidation();
            return messages;
        }

        private void RunValidation()
        {
            if (runValidation)
            {
                Validate();
                runValidation = false;
            }
        }
    }
}
