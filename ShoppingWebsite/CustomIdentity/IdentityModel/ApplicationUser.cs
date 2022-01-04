using CustomIdentity.IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomIdentity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {

        }

        public virtual ICollection<UserClaims> UserClaim { get; set; }
        public virtual ICollection<UserLogin> UserLogin { get; set; }
        public virtual ICollection<UserToken> UserToken { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }


}
