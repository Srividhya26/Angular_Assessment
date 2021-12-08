using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsiteDA.Model;
using ShoppingWebsiteDA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWebsiteDA.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,Roles,Guid>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                //b.HasKey(u => u.Id);

                b.ToTable("Users");

            });

            builder.Entity<UserClaims>(b =>
            {
                //b.HasKey(uc => uc.Id);

                b.ToTable("UserClaims");
            });

            builder.Entity<ViewModel.UserLogin>(b =>
            {
                //b.HasKey(ul => ul.LoginProvider);

                b.ToTable("UserLogins");
            });

            builder.Entity<Roles>(b =>
            {
                //b.HasKey(r => r.Id);

                b.ToTable("Roles");
            });

            builder.Entity<ViewModel.RoleClaim>(b =>
            {
                //b.HasKey(rc => rc.Id);

                b.ToTable("RoleClaims");
            });

            builder.Entity<UserRole>(b =>
            {
                //b.HasKey(ur => ur.UserId);

                b.ToTable("UsersRole");
            });
        }
    }

   
}
