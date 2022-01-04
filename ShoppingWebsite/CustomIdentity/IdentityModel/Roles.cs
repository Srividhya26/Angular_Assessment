using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomIdentity
{
    public class Roles : IdentityRole<Guid>
    {
        public Roles()
        {

        }

        public Roles(string roleName) : base(roleName)
        {

        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaim { get; set; }
    }
}
