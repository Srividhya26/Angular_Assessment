using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoppingWebsiteDA.Model
{
    public partial class Role
    {
        public Role()
        {
            RoleClaims = new HashSet<RoleClaim>();
            UsersRoles = new HashSet<UsersRole>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        [InverseProperty(nameof(RoleClaim.Role))]
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        [InverseProperty(nameof(UsersRole.Role))]
        public virtual ICollection<UsersRole> UsersRoles { get; set; }
    }
}
