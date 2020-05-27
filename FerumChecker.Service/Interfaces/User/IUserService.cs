using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.Identity;
using FerumChecker.Service.DTO;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FerumChecker.Service.Interfaces.User
{
    public interface IUserService : IDisposable
    {
        UserProfile Get(string id);
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);

        ApplicationUserManager GetApplicationUserManager();
    }
}
