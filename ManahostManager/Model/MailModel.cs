using System;
using System.Collections.Generic;

namespace ManahostManager.Model
{
    public class MailModel
    {
        // List of people ids
        public List<int> To { get; set; }

        // Subject of the mail
        public String Subject { get; set; }

        // Body of the mail
        public String Body { get; set; }

        // Id of the mailconfig that the manager wants to use
        public int MailConfigId { get; set; }

        // Password in Base64
        // Password of its mail account
        public String Password { get; set; }

        // document ids
        public List<int> Attachments { get; set; }
    }
}