using FerumChecker.DataAccess.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Repository.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider servises, Microsoft.Extensions.Logging.ILogger<UserManager<ApplicationUser>> logger)
                : base(store, optionAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, servises, logger)
        {
        }
    }
}
