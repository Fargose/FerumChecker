using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.Service.Interfaces.Infrastructure;
using FerumChecker.Service.Interfaces.User;
using FerumChecker.Web.ViewModel.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FerumChecker.Web.ViewModel.Infrastructure.ComputerAssemblyViewModel;

namespace FerumChecker.Web.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        private readonly IComputerAssemblyService _computerAssemblyService;
        private readonly IUserService _userService;

        public EditorController(IComputerAssemblyService computerAssemblyService, IUserService userService)
        {
            _computerAssemblyService = computerAssemblyService;
            _userService = userService;
        }
        public IActionResult List()
        {
            var model = _computerAssemblyService.GetComputerAssemblies().Select(m => new ComputerAssemblyViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                UserId = m.OwnerId
            });

            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var userId = _userService.GetApplicationUserManager().GetUserId(this.User);

            return View(model.Where(m => m.UserId == userId));
        }
        public IActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                var computerAssembly = _computerAssemblyService.GetComputerAssembly(id.Value);
                if (computerAssembly == null)
                {
                    return NotFound();
                }

                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                var userId = _userService.GetApplicationUserManager().GetUserId(this.User);
                if (computerAssembly.OwnerId != userId)
                {
                    return Forbid();
                }

                var model = new ComputerAssemblyViewModel();
                model.Id = computerAssembly.Id;
                model.Name = computerAssembly.Name;
                model.UserId = computerAssembly.OwnerId;
                model.CPUId = computerAssembly.CPUId;
                model.CPUName = computerAssembly.CPU == null ? "Пусто" : computerAssembly.CPU.Name;
                model.CPUImage = computerAssembly.CPU == null ? "" : "/Images/CPU/" + computerAssembly.CPU.Image;
                model.MotherBoardId = computerAssembly.MotherBoardId;
                model.MotherBoardName = computerAssembly.MotherBoard == null ? "Пусто" : computerAssembly.MotherBoard.Name;
                model.MotherBoardImage = computerAssembly.MotherBoard == null ? "" : "/Images/MotherBoard/" + computerAssembly.MotherBoard.Image;
                model.PCCaseId = computerAssembly.PCCaseId;
                model.PCCaseName = computerAssembly.PCCase == null ? "Пусто" : computerAssembly.PCCase.Name;
                model.PCCaseImage = computerAssembly.PCCase == null ? "" : "/Images/PCCase/" + computerAssembly.PCCase.Image;
                model.PowerSupplyId = computerAssembly.PowerSupplyId;
                model.PowerSupplyName = computerAssembly.PowerSupply == null ? "Пусто" : computerAssembly.PowerSupply.Name;
                model.PowerSupplyImage = computerAssembly.PowerSupply == null ? "" : "/Images/PowerSupply/" + computerAssembly.PowerSupply.Image;
                model.VideoCards = computerAssembly.VideoCards.Select(m => new VideoCardShortModel(m.VideoCard)).ToList();
                model.RAMs = computerAssembly.ComputerAssemblyRAMs.Select(m => new RAMShortModel(m.RAM)).ToList();
                model.SSDs = computerAssembly.SSDs.Select(m => new SSDShortModel(m.SSD)).ToList();
                model.HDDs = computerAssembly.HDDs.Select(m => new HDDShortModel(m.HDD)).ToList();
                model.RAMFreeSlot = _computerAssemblyService.CalculateFreeRAMSlot(computerAssembly);
                model.OuterMemoryFreeSlot = _computerAssemblyService.CalculateFreeOuterMemorySlot(computerAssembly);
                return View(model);
            }
            else
            {
                var computerAssembly = new ComputerAssembly();
                computerAssembly.Name = "Безіменна збірка від " + DateTime.Now.ToLongDateString();
                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                var userId = _userService.GetApplicationUserManager().GetUserId(this.User);
                computerAssembly.OwnerId = userId;
                _computerAssemblyService.CreateComputerAssembly(computerAssembly);
                var model = new ComputerAssemblyViewModel();
                model.Id = computerAssembly.Id;
                model.Name = computerAssembly.Name;
                model.UserId = computerAssembly.OwnerId;
                return View(model);
            }
        }


        public IActionResult Details(int id)
        {
            var computerAssembly = _computerAssemblyService.GetComputerAssembly(id);
            if (computerAssembly == null)
            {
                return NotFound();
            }

            var model = new ComputerAssemblyViewModel();
            model.Id = computerAssembly.Id;
            model.Name = computerAssembly.Name;
            model.UserId = computerAssembly.OwnerId;
            model.CPUId = computerAssembly.CPUId;
            model.CPUName = computerAssembly.CPU == null ? "Пусто" : computerAssembly.CPU.Name;
            model.CPUImage = computerAssembly.CPU == null ? "" : "/Images/CPU/" + computerAssembly.CPU.Image;
            model.MotherBoardId = computerAssembly.MotherBoardId;
            model.MotherBoardName = computerAssembly.MotherBoard == null ? "Пусто" : computerAssembly.MotherBoard.Name;
            model.MotherBoardImage = computerAssembly.MotherBoard == null ? "" : "/Images/MotherBoard/" + computerAssembly.MotherBoard.Image;
            model.PCCaseId = computerAssembly.PCCaseId;
            model.PCCaseName = computerAssembly.PCCase == null ? "Пусто" : computerAssembly.PCCase.Name;
            model.PCCaseImage = computerAssembly.PCCase == null ? "" : "/Images/PCCase/" + computerAssembly.PCCase.Image;
            model.PowerSupplyId = computerAssembly.PowerSupplyId;
            model.PowerSupplyName = computerAssembly.PowerSupply == null ? "Пусто" : computerAssembly.PowerSupply.Name;
            model.PowerSupplyImage = computerAssembly.PowerSupply == null ? "" : "/Images/PowerSupply/" + computerAssembly.PowerSupply.Image;
            model.VideoCards = computerAssembly.VideoCards.Select(m => new VideoCardShortModel(m.VideoCard)).ToList();
            model.RAMs = computerAssembly.ComputerAssemblyRAMs.Select(m => new RAMShortModel(m.RAM)).ToList();
            model.SSDs = computerAssembly.SSDs.Select(m => new SSDShortModel(m.SSD)).ToList();
            model.HDDs = computerAssembly.HDDs.Select(m => new HDDShortModel(m.HDD)).ToList();
            model.RAMFreeSlot = _computerAssemblyService.CalculateFreeRAMSlot(computerAssembly);
            model.OuterMemoryFreeSlot = _computerAssemblyService.CalculateFreeOuterMemorySlot(computerAssembly);
            model.OwnerName = computerAssembly.Owner.Name + " " + computerAssembly.Owner.Surname;
            return View(model);
        }


        public ActionResult GetRecomendation(int assemblyId)
        {
            var computerAssembly = _computerAssemblyService.GetComputerAssembly(assemblyId);
            if (computerAssembly == null)
            {
                return NotFound();
            }
            var userId = _userService.GetApplicationUserManager().GetUserId(this.User);
            if (computerAssembly.OwnerId != userId)
            {
                return Forbid();
            }
            var result = _computerAssemblyService.CreateRecomendations(computerAssembly);
            return PartialView("Recomendation", result);
        }
    }
}