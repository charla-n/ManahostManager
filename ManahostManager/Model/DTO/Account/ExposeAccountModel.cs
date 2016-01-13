using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using System;
using System.Collections.Generic;

namespace ManahostManager.Model.DTO.Account
{
    public class ExposeAccountModel
    {
        public ExposeAccountModel()
        {
            Claims = new List<KeyValuePair<string, string>>();
        }

        public string Email { get; set; }

        public int? DefaultHomeId { get; set; }

        public Boolean? AcceptMailing { get; set; }

        public String Civility { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Country { get; set; }

        public String Locale { get; set; }

        public double Timezone { get; set; }

        public PhoneNumber PrincipalPhone { get; set; }

        public List<PhoneNumber> SecondaryPhone { get; set; }

        public bool InitManager { get; set; }

        public bool TutorialManager { get; set; }

        public List<KeyValuePair<string, string>> Claims { get; set; }

        public HomeDTO HomeDefault { get; set; }
    }
}