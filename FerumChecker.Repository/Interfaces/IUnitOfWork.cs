using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FerumChecker.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ComputerAssembly> ComputerAssemblies { get; }
        ApplicationUserManager ApplicationUsers { get; }
        IRepository<UserProfile> UserProfiles { get; }
        ApplicationRoleManager Roles { get; }
        void Save();

        Task SaveAsync();
    }
}
