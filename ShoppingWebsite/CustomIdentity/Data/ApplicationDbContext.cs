using CustomIdentity.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomIdentity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,Roles,Guid,UserClaims,UserRole,UserLogin,RoleClaim,UserToken>
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

            builder.Entity<UserLogin>(b =>
            {
                //b.HasKey(ul => ul.LoginProvider);

                b.ToTable("UserLogins");
            });

            builder.Entity<Roles>(b =>
            {
                //b.HasKey(r => r.Id);

                b.ToTable("Roles");
            });

            builder.Entity<RoleClaim>(b =>
            {
                //b.HasKey(rc => rc.Id);

                b.ToTable("RoleClaims");
            });

            builder.Entity<UserRole>(b =>
            {
                //b.HasKey(ur => ur.UserId);

                b.ToTable("UsersRole");
            });

            builder.Entity<UserToken>(b =>
            {
                b.ToTable("UserToken");
            });
        }
    }

   
}
