using Microsoft.AspNet.Identity.EntityFramework;

namespace NewsSite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? CategoryId { get; set; }
        public byte [] UserPhoto { get; set; }
    }   
}