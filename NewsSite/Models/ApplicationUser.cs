using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsSite.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsSite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? CategoryId { get; set; }
        public byte [] UserPhoto { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }   
}