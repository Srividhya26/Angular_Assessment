using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomIdentity.IdentityModel
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public UserToken()
        {

        }

        public virtual ApplicationUser User { get; set; }
    }
}
