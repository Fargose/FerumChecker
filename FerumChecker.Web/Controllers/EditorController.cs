using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Service.Interfaces.Hardware;
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
        private readonly ICommentService _commentService;

        public EditorController(IComputerAssemblyService computerAssemblyService, IUserService userService, ICommentService commentService)
        {
            _computerAssemblyService = computerAssemblyService;
            _userService = userService;
            _commentService = commentService;
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
        public IActionResult Index(int? id, string name)
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
                model.VideoCards = computerAssembly.VideoCards.Select(m => new VideoCardShortModel(m.VideoCard)).ToList() ?? new List<VideoCardShortModel>();
                model.RAMs = computerAssembly.ComputerAssemblyRAMs.Select(m => new RAMShortModel(m.RAM)).ToList() ?? new List<RAMShortModel>();
                model.SSDs = computerAssembly.SSDs.Select(m => new SSDShortModel(m.SSD)).ToList() ?? new List<SSDShortModel>();
                model.HDDs = computerAssembly.HDDs.Select(m => new HDDShortModel(m.HDD)).ToList() ?? new List<HDDShortModel>();
                model.RAMFreeSlot = _computerAssemblyService.CalculateFreeRAMSlot(computerAssembly);
                model.OuterMemoryFreeSlot = _computerAssemblyService.CalculateFreeOuterMemorySlot(computerAssembly);
                model.Public = computerAssembly.Public;
                return View(model);
            }
            else
            {
                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                var userId = _userService.GetApplicationUserManager().GetUserId(this.User);
                var computerAssembly = new ComputerAssembly();
                var computerAssemblies = _computerAssemblyService.GetComputerAssemblies().Where(m => m.OwnerId == userId &&  m.Name == name);
                if (computerAssemblies.Any())
                {
                    return Json(new { error = "Ви вже створили збірку з цією назвою" });
                }
                computerAssembly.Name = name;
                computerAssembly.OwnerId = userId;
                _computerAssemblyService.CreateComputerAssembly(computerAssembly);
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
                model.VideoCards =  new List<VideoCardShortModel>();
                model.RAMs = new List<RAMShortModel>();
                model.SSDs =  new List<SSDShortModel>();
                model.HDDs =  new List<HDDShortModel>();
                model.RAMFreeSlot = 1;
                model.OuterMemoryFreeSlot = 1;
                model.Public = false;
                return Json(model);
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
            var userId = _userService.GetApplicationUserManager().GetUserId(this.User);
            model.Comments = computerAssembly.Comments.Select(m => new CommentViewModel(m, userId)).ToList();
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

        [HttpGet]

        public IActionResult CreateComment()
        {
            return PartialView();
        }

        [HttpPost]

        public IActionResult CreateComment(CommentViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment();
                comment.Text = model.Text;
                comment.ComputerAssemblyId = model.ComputerAssemblyId.Value;
                var userId = _userService.GetApplicationUserManager().GetUserId(this.User);
                comment.OwnerId = userId;
                _commentService.CreateComment(comment);
                comment = _commentService.GetComment(comment.Id);

                return Json(new CommentViewModel(comment, userId));
            }

            return PartialView(model);
        }

        public ActionResult CommentDelete(int id)
        {
            var comment = _commentService.GetComment(id);
            if(comment == null)
            {
                return NotFound();
            }

            var userId = _userService.GetApplicationUserManager().GetUserId(this.User);
            if (userId != comment.OwnerId)
            {
                return Forbid();
            }

            var result = _commentService.DeleteComment(id);
            return Json(result);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var computerAssembly = _computerAssemblyService.GetComputerAssembly(id);
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

            var result = _computerAssemblyService.DeleteComputerAssembly(id);

            return Json(result);

        }

        [HttpPost]
        public ActionResult SetPublic(int id, bool isPublic)
        {
            var computerAssembly = _computerAssemblyService.GetComputerAssembly(id);
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

            computerAssembly.Public = isPublic;
            var result = _computerAssemblyService.UpdateComputerAssembly(computerAssembly);

            return Json(result);

        }

        [HttpGet]

        public ActionResult AssemblyStat(int? assemblyId)
        {
            var computerAssembly = _computerAssemblyService.GetComputerAssembly(assemblyId.Value);
            if (computerAssembly == null)
            {
                return NotFound();
            }

            var model = new ComputerAssemblyStatModel();
            model.CPUFrequency = computerAssembly.CPU != null ? ((double)computerAssembly.CPU.Frequency / 1000) + " GHz" : " - ";
            model.CPUCores = computerAssembly.CPU != null ? computerAssembly.CPU.CoresNumber.ToString() : " - "; 
            model.CPUThreads = computerAssembly.CPU != null ? computerAssembly.CPU.ThreadsNumber.ToString() : " - ";
            model.VideoMemory = computerAssembly.VideoCards.Count() > 0 ? CreateVideoMemoryDescription(computerAssembly.VideoCards.ElementAt(0).VideoCard.MemorySize) : " - ";
            model.VideoMemoryFrequency = computerAssembly.VideoCards.Count() > 0 ? computerAssembly.VideoCards.ElementAt(0).VideoCard.MemoryFrequency + " MHz" : " - ";
            model.VideoFrequency = computerAssembly.VideoCards.Count() > 0 ? computerAssembly.VideoCards.ElementAt(0).VideoCard.Frequency + " MHz" : " - "; 
            model.TotalRam = computerAssembly.ComputerAssemblyRAMs.Count() > 0 ? CreateVideoMemoryDescription(_computerAssemblyService.GetTotalRAM(computerAssembly)) : " - ";
            model.TotalMemory = (computerAssembly.SSDs.Count() > 0 || computerAssembly.HDDs.Count() > 0) ? CreateOuterMemoryDescription(_computerAssemblyService.GetTotalVolume(computerAssembly)) : " - " ;
            model.TotalPrice = _computerAssemblyService.CalculatePrice(computerAssembly);


            return PartialView(model);

        }


        private string CreateVideoMemoryDescription(int memory)
        {
            if (memory > 1024)
            {
                return memory / 1024 + "ГБ";
            }
            return memory + " МБ";
        }

        private string CreateOuterMemoryDescription(int memory)
        {
            if (memory > 1024)
            {
                return memory / 1024 + "TБ";
            }
            return memory + " ГБ";
        }
    }
}