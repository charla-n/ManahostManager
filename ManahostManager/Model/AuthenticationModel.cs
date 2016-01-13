using System;

namespace ManahostManager.Model
{
    public class AuthenticationModel
    {
        public String Remoteip { get; set; }

        public String Challenge { get; set; }

        public String Response { get; set; }
    }
}