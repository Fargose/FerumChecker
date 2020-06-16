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
using FerumChecker.Web.ViewModel.Search;

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
            return View();
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

        public IActionResult AssemblyTable(AssemblySearchModel searchDetails)
        {
            var computers = _computerAssemblyService.GetComputerAssemblies().Where(m => m.Public);
            if (searchDetails != null)
            {

                if (searchDetails.MinPrice.HasValue)
                {
                    computers = computers.Where(m => _computerAssemblyService.CalculatePrice(m) >= searchDetails.MinPrice);
                }
                if (searchDetails.MaxPrice.HasValue)
                {
                    computers = computers.Where(m => _computerAssemblyService.CalculatePrice(m) <= searchDetails.MaxPrice);
                }
                if (searchDetails.MinFrequency.HasValue)
                {
                    computers = computers.Where(m => m.CPU.Frequency >= searchDetails.MinFrequency);
                }
                if (searchDetails.MaxFrequency.HasValue)
                {
                    computers = computers.Where(m => m.CPU.Frequency <= searchDetails.MaxFrequency);
                }
                if (searchDetails.MinCores.HasValue)
                {
                    computers = computers.Where(m => m.CPU.CoresNumber >= searchDetails.MinCores);
                }
                if (searchDetails.MaxCores.HasValue)
                {
                    computers = computers.Where(m => m.CPU.CoresNumber <= searchDetails.MaxCores);
                }
                if (searchDetails.MinVideoMemory.HasValue)
                {
                    computers = computers.Where(m => m.VideoCards.ElementAt(0).VideoCard.MemorySize >= searchDetails.MinVideoMemory);
                }
                if (searchDetails.MaxVideoMemory.HasValue)
                {
                    computers = computers.Where(m => m.VideoCards.ElementAt(0).VideoCard.MemorySize <= searchDetails.MaxVideoMemory);
                }
                if (searchDetails.MinRAM.HasValue)
                {
                    computers = computers.Where(m => _computerAssemblyService.GetTotalRAM(m) >= searchDetails.MinRAM);
                }
                if (searchDetails.MaxRAM.HasValue)
                {
                    computers = computers.Where(m => _computerAssemblyService.GetTotalRAM(m) >= searchDetails.MaxRAM);
                }
                if (searchDetails.MinVolume.HasValue)
                {
                    computers = computers.Where(m => _computerAssemblyService.GetTotalVolume(m) >= searchDetails.MinVolume);
                }
                if (searchDetails.MaxVolume.HasValue)
                {
                    computers = computers.Where(m => _computerAssemblyService.GetTotalVolume(m) <= searchDetails.MaxVolume);
                }

            }
            var model = computers.Select(m => new ComputerAssemblyViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                UserId = m.OwnerId,
                OwnerName = m.Owner.Name + " " + m.Owner.Surname
            });

            model = model.Where(m => string.IsNullOrEmpty(searchDetails.Name) || m.Name.Contains(searchDetails.Name) || m.OwnerName.Contains(searchDetails.Name));

            return PartialView(model);
        }

        public IActionResult Search()
        {
            return PartialView();
        }
    }
}
