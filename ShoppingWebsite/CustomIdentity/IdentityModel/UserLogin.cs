using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomIdentity
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public UserLogin()
        {

        }

        public virtual ApplicationUser User { get; set; }
    }
}
