using ManahostManager.Domain.Tools;
using Microsoft.AspNet.Identity.EntityFramework;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    public class Client : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public Client()
        {
            PrincipalPhone = null;
            SecondaryPhone = new List<PhoneNumber>();
            LastAttemptConnexion = null;
            LockoutEnabled = false;
            LockoutEndDateUtc = null;
            this.AccessFailedCount = 0;
        }

        public int? DefaultHomeId { get; set; }

        public override string UserName
        {
            get
            {
                return Email;
            }

            set
            {
                Email = value;
            }
        }

        public override string PhoneNumber
        {
            get
            {
                if (PrincipalPhone == null)
                    return null;
                return PrincipalPhone.Phone;
            }

            set
            {
                if (PrincipalPhone != null)
                {
                    PrincipalPhone.PhoneType = PhoneType.HOME;
                    PrincipalPhone.Phone = value;
                }
            }
        }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Boolean IsManager { get; set; }

        public Boolean AcceptMailing { get; set; }

        public String Civility { get; set; }

        public String Country { get; set; }

        public DateTime? DateBirth { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public DateTime DateCreation { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Locale { get; set; }

        public PhoneNumber PrincipalPhone { get; set; }

        public List<PhoneNumber> SecondaryPhone { get; set; }

        public double Timezone { get; set; }

        public DateTime? DateModification { get; set; }

        public bool InitManager { get; set; }
        public bool TutorialManager { get; set; }
        public DateTime? LastAttemptConnexion { get; set; }
    }
}