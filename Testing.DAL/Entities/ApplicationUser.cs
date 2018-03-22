using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Testing.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual StudentProfile StudentProfile { get; set; }
       
    }
}