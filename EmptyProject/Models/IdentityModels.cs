using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmptyProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        // Add Student Custom Fields Here
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Mobile { get; set; }
        public Inst RegisterAs { get; set; }

        // Add institute Custom Fields Here
        public int Inst_Id { get; set; }
        public string Inst_Code { get; set; }
        public string Inst_Name { get; set; }
        public string Inst_Address { get; set; }
        public int User_Id { get; set; }
        public string User_Email { get; set; }
        public string Passw { get; set; }
    }
    public enum Inst
    {
        admin,
        teacher,
        student
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Institute> institutes { get; set; }
        public virtual DbSet<User> user_data { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}