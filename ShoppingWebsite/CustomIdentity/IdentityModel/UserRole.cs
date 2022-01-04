using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomIdentity
{
    public class UserRole : IdentityUserRole<Guid>
    {

        public UserRole()
        {

        }

        public virtual ApplicationUser User { get; set; }
        public virtual Roles Role { get; set; }
    }
}
