using System;
using System.Collections.Generic;

namespace ManahostManager.Domain.DTOs
{
    public class ClientDTO
    {
        public int? DefaultHomeId { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public Boolean AcceptMailing { get; set; }

        public String Civility { get; set; }

        public String Country { get; set; }

        public DateTime? DateBirth { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Locale { get; set; }

        public PhoneNumberDTO PrincipalPhone { get; set; }

        public List<PhoneNumberDTO> SecondaryPhone { get; set; }

        public String Timezone { get; set; }

        public DateTime? DateModification { get; set; }

        public bool InitManager { get; set; }
        public bool TutorialManager { get; set; }
        public DateTime? LastAttemptConnexion { get; set; }
    }
}