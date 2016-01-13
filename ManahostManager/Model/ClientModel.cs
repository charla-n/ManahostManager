using ManahostManager.Domain.Entity;
using System;

namespace ManahostManager.Model
{
    public class ClientModel
    {
        public Client Client { get; set; }

        public String Remoteip { get; set; }

        public String Challenge { get; set; }

        public String Response { get; set; }
    }
}