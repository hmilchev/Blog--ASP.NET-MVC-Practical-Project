using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog__ASP.NET_MVC_Practical_Project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
   

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Blog__ASP.NET_MVC_Practical_Project.Models.Post> Posts { get; set; }

        //public System.Data.Entity.DbSet<Blog__ASP.NET_MVC_Practical_Project.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}