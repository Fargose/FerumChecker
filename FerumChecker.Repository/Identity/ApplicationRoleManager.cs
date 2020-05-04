using FerumChecker.DataAccess.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Repository.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, Microsoft.Extensions.Logging.ILogger<RoleManager<ApplicationRole>> logger)
                    : base(store, roleValidators, keyNormalizer, errors, logger)
        { }
    }
}
