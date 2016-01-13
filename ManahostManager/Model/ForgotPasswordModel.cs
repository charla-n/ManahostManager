using System;

namespace ManahostManager.Model
{
    public class ForgotPasswordModel
    {
        public String Email { get; set; }

        public String Remoteip { get; set; }

        public String Challenge { get; set; }

        public String Response { get; set; }
    }
}