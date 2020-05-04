using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.DataAccess.Entities.User
{
    public class ApplicationUser: IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
    }
}
