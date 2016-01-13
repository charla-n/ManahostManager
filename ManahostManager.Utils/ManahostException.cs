using System;

namespace ManahostManager.Utils
{
    public class ManahostException : Exception
    {
        public String[] Errors { get; set; }

        public ManahostException(String msg) : base()
        {
            Errors = new string[] { msg };
        }

        public ManahostException() : base()
        {
        }

        public ManahostException(String[] errors) : base()
        {
            this.Errors = errors;
        }
    }
}