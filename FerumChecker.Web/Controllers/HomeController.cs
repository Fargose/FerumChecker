using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FerumChecker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using FerumChecker.Service.Interfaces.Infrastructure;
using FerumChecker.Web.ViewModel.Infrastructure;
using FerumChecker.Service.Interfaces.User;

namespace FerumChecker.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IComputerAssemblyService _computerAssemblyService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IComputerAssemblyService computerAssemblyService, IUserService userService)
        {
            _logger = logger;
            _computerAssemblyService = computerAssemblyService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var model = _computerAssemblyService.GetComputerAssemblies().Where(m => m.Public).Select(m => new ComputerAssemblyViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                UserId = m.OwnerId,
                OwnerName = m.Owner.Name + " " + m.Owner.Surname
            });


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
