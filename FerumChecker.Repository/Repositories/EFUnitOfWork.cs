using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Identity;
using FerumChecker.Repository.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FerumChecker.Repository.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;


        public EFUnitOfWork(ApplicationContext context)
        {
            db = context;
        }

        public EFUnitOfWork(ApplicationContext db, Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> optionAccessor, Microsoft.AspNetCore.Identity.IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<Microsoft.AspNetCore.Identity.IUserValidator<ApplicationUser>> userValidators, IEnumerable<Microsoft.AspNetCore.Identity.IPasswordValidator<ApplicationUser>> passwordValidators, Microsoft.AspNetCore.Identity.ILookupNormalizer keyNormalizer, Microsoft.AspNetCore.Identity.IdentityErrorDescriber errors, IServiceProvider servises, Microsoft.Extensions.Logging.ILogger<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>> logger, IEnumerable<Microsoft.AspNetCore.Identity.IRoleValidator<ApplicationRole>> roleValidators, Microsoft.Extensions.Logging.ILogger<Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole>> roleLogger)
        {
            this.db = db;
            ComputerAssemblies = new ComputerAssemblyRepository(db);

            ApplicationUsers = new ApplicationUserManager(new UserStore<ApplicationUser>(db), optionAccessor, passwordHasher, userValidators, passwordValidators,keyNormalizer, errors, servises, logger);
            Roles = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db), roleValidators, keyNormalizer, errors, roleLogger);
            UserProfiles = new UserProfileRepository(db);
        }
        public IRepository<ComputerAssembly> ComputerAssemblies { get; }


        public ApplicationUserManager ApplicationUsers { get; }

        public IRepository<UserProfile> UserProfiles { get; }

        public ApplicationRoleManager Roles { get; }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

