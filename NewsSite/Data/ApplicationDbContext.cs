using Microsoft.AspNet.Identity.EntityFramework;
using NewsSite.Models;
using System.Data.Entity;

namespace NewsSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Category> Categories { get; set; }
    }
   
}