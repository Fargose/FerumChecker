using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.DTO;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces;
using FerumChecker.Service.Interfaces.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FerumChecker.Service.Services.user
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.ApplicationUsers.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.ApplicationUsers.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                // добавляем роль
                await Database.ApplicationUsers.AddToRoleAsync(user, "User");
                // создаем профиль клиента
                UserProfile clientProfile = new UserProfile { Id = user.Id, Name = userDto.Name };
                Database.UserProfiles.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.ApplicationUsers.FindByEmailAsync(userDto.Email);

            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null  && await Database.ApplicationUsers.CheckPasswordAsync(user, userDto.Password))
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                

                claim = identity;

                
            }

            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.Roles.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.Roles.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
