using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seed Roles(User,Admin,SuperAdmin
            var adminRoleId = "7bf37f2c-783c-4b6e-b7e9-fbe9a643ec52";
            var superAdminRoleId = "6409b749-eaf4-4a09-b03b-f756519e249e";
            var userRoleId = "0bdbbc4f-4a25-43fb-a8fc-aae7bc4fe41d";
            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            //Seed SuperAdminUser
            var superAdminId = "a9fb0801-8411-4b6f-bf94-e023d850405e";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@blogger.com",
                Email = "superadmin@blogger.com",
                NormalizedEmail = "superadmin@blogger.com".ToUpper(),
                NormalizedUserName = "superadmin@blogger.com",
                Id = superAdminId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser,"Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //Add all roles to SuperAdminUser
        }
    }
}
