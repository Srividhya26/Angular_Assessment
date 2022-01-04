using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoppingWebsiteDA.Model
{
    public partial class User
    {
        public User()
        {
            Brands = new HashSet<Brand>();
            Categories = new HashSet<Category>();
            UserClaims = new HashSet<UserClaim>();
            UserLogins = new HashSet<UserLogin>();
            UserTokens = new HashSet<UserToken>();
            UsersRoles = new HashSet<UsersRole>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string NormalizedUserName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        [InverseProperty(nameof(Brand.User))]
        public virtual ICollection<Brand> Brands { get; set; }
        [InverseProperty(nameof(Category.User))]
        public virtual ICollection<Category> Categories { get; set; }
        [InverseProperty(nameof(UserClaim.User))]
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        [InverseProperty(nameof(UserLogin.User))]
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        [InverseProperty(nameof(UserToken.User))]
        public virtual ICollection<UserToken> UserTokens { get; set; }
        [InverseProperty(nameof(UsersRole.User))]
        public virtual ICollection<UsersRole> UsersRoles { get; set; }
    }
}
