using Microsoft.AspNet.Identity;
using Testing.DAL.Entities;

namespace Testing.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }

    }
}
