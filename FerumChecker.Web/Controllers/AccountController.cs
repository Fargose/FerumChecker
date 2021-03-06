﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Service.DTO;
using FerumChecker.Service.Interfaces;
using FerumChecker.Service.Interfaces.User;
using FerumChecker.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FerumChecker.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService  userService;

        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, IUserService userService)
        {

            this.signInManager = signInManager;
            this.userService = userService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO
                {
                    Password = model.Password,
                    Email = model.Email,
                    UserName = model.Email,
                    Name = model.Name,
                    SurName = model.SurName

                };
                if(User.IsInRole("Administrator") && model.isAdmin)
                {
                    user.Role = "Administrator";
                }
                var result = await userService.Create(user);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("Email", "Користувач з такою електронною адресою вже існує.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                   
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильний email або пароль, спробуйте ще раз.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}