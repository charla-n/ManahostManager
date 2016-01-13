using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils.ServiceMessage;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ManahostManager.Domain.Tools
{
    public class CustomUserRole : IdentityUserRole<int> { }

    public class CustomUserClaim : IdentityUserClaim<int> { }

    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole()
        {
        }

        public CustomRole(string name)
        {
            Name = name;
        }
    }

    public class CustomUserStore : UserStore<Client, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ManahostManagerDAL context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ManahostManagerDAL context)
            : base(context)
        {
        }
    }

    public class ClientUserManager : UserManager<Client, int>
    {
        private static readonly string HASH_DATA_PROTECTION_PROVIDER = "40f0423f46b419ed0a2b987af1cc794d";
        private static readonly string HASH_DAPI = "e159405205ed2ea683d00f926c35fceb";

        public ClientUserManager(IUserStore<Client, int> store)
            : base(store)
        {
            this.UserValidator = new UserValidator<Client, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            this.PasswordHasher = new BcryptPasswordHasher();
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            this.EmailService = new EmailService();
            this.UserTokenProvider = new EmailTokenProvider<Client, int>();
        }

        public Client FindUserByIdWithPhoneNumber(int key)
        {
            return Users.Include("PrincipalPhone").Include("SecondaryPhone").Where(x => x.Id == key).FirstOrDefault();
        }

        public Task<Client> FindUserByIdWithPhoneNumberAsync(int key)
        {
            return Users.Include("PrincipalPhone").Include("SecondaryPhone").Where(x => x.Id == key).FirstOrDefaultAsync();
        }
    }

    public class ClientRoleManager : RoleManager<CustomRole, int>
    {
        public ClientRoleManager(IRoleStore<CustomRole, int> roleStore)
            : base(roleStore)
        {
        }
    }
}