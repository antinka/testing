using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Testing.DAL.Entities;

namespace Testing.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
    }
}
