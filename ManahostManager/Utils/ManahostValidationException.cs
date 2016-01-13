using System;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Utils
{
    public class ManahostValidationException : Exception
    {
        public ModelStateDictionary ModelState { get; set; }

        public String AdditionalMessage { get; set; }

        public ManahostValidationException(ModelStateDictionary obj) : base("Validation Failed")
        {
            this.ModelState = obj;
        }

        public ManahostValidationException(ModelStateDictionary obj, String additionalMessage)
            : base("Validation Failed")
        {
            this.ModelState = obj;
            this.AdditionalMessage = additionalMessage;
        }
    }
}