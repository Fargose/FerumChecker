using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Service.Interfaces.Infrastructure;
using FerumChecker.Service.Interfaces.User;
using FerumChecker.Service.Services.Infrastructure;
using FerumChecker.Web.Code;
using FerumChecker.Web.ViewModel.Hardware;
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{

    public class PowerSupplyController : Controller
    {

        private readonly IPowerSupplyService _powerSupplyService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPowerSupplyMotherBoardInterfaceService _motherBoardPowerSupplyInterfaceService;
        private readonly IPowerSupplyCPUInterfaceService _powerSupplyCPUInterfaceService;
        private readonly IComputerAssemblyService _computerAssemblyService;
        private readonly IUserService _userService;
        public PowerSupplyController(IPowerSupplyService powerSupplyService, IWebHostEnvironment hostEnvironment, IManufacturerService manufacturerService, IPowerSupplyMotherBoardInterfaceService motherBoardPowerSupplyInterfaceService, IPowerSupplyCPUInterfaceService powerSupplyCPUInterfaceService, IComputerAssemblyService computerAssemblyService, IUserService userService)
        {
            _powerSupplyService = powerSupplyService;
            _webHostEnvironment = hostEnvironment;
            _manufacturerService = manufacturerService;
            _motherBoardPowerSupplyInterfaceService = motherBoardPowerSupplyInterfaceService;
            _powerSupplyCPUInterfaceService = powerSupplyCPUInterfaceService;
            _computerAssemblyService = computerAssemblyService;
            _userService = userService;
        }


        public ActionResult SetHardware(int id, int assemblyId)
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
            var result = _computerAssemblyService.SetPowerSupply(id, assemblyId);
            return Json(result);
        }

        public ActionResult RemoveHardware(int assemblyId)
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
            var result = _computerAssemblyService.RemovePowerSupply(computerAssembly);
            return Json(result);
        }
        public IActionResult SmallList()
        {
            var cpus = _powerSupplyService.GetPowerSupplies().OrderBy(m => m.Name);

            var model = cpus.Select(m => new PowerSupplyViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ImagePath = "/Images/PowerSupply/" + m.Image
            });

            return PartialView(model);
        }
        // GET: PowerSupply
        public ActionResult Index()
        {
            var powerSupplys = _powerSupplyService.GetPowerSupplies().OrderBy(m => m.Name);

            var model = powerSupplys.Select(m => new PowerSupplyViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: PowerSupply
        public ActionResult PartialIndex(string search = "")
        {
            var powerSupplys = _powerSupplyService.GetPowerSupplies().OrderBy(m => m.Name);
            var model = powerSupplys.Select(m => new PowerSupplyViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ShortDescription = string.IsNullOrEmpty(m.Description) ? "" : CreateShortDescription(m.Description),
                Manufacturer = m.Manufacturer.Name,
                PowerDisplay   = m.Power +  " Вт",
                ImagePath = "/Images/PowerSupply/" + m.Image,
                Price = m.Price
            }).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search));

            return PartialView("Index", model);
        }

        // GET: PowerSupply/Details/5
        public ActionResult Details(int id)
        {
            var powerSupply = _powerSupplyService.GetPowerSupply(id);
            if(powerSupply == null)
            {
                return NotFound();
            }
            var model = new PowerSupplyViewModel()
            {
                Id = powerSupply.Id,
                Name = powerSupply.Name,
                PowerDisplay = powerSupply.Power + " Вт",
                PowerSupplyMotherBoardInterface = powerSupply.PowerSupplyMotherBoardInterface.Name,
                CoolerSizeDisplay = powerSupply.CoolerSize + " mm",
                GPUInputNumber = powerSupply.GPUInputNumber,
                SATAInputNumber = powerSupply.SATAInputNumber,
                Manufacturer = powerSupply.Manufacturer.Name,         
                //PowerSupplyCPUInterfaces = powerSupply.PowerSupplyPowerSupplyCPUInterfaces.Select(m => new PowerSupplyCPUInterfaceViewModel { Id = m.PowerSupplyCPUInterface.Id, Name = m.PowerSupplyCPUInterface.Name }).ToList(),
                Price = powerSupply.Price,
                ImagePath = "/Images/PowerSupply/" + powerSupply.Image,
                Description = powerSupply.Description
            };

            return View(model);
        }


        public ActionResult PartialDetails(int id)
        {
            var powerSupply = _powerSupplyService.GetPowerSupply(id);
            if (powerSupply == null)
            {
                return NotFound();
            }
            var model = new PowerSupplyViewModel()
            {
                Id = powerSupply.Id,
                Name = powerSupply.Name,
                PowerDisplay = powerSupply.Power + " Вт",
                PowerSupplyMotherBoardInterface = powerSupply.PowerSupplyMotherBoardInterface.Name,
                CoolerSizeDisplay = powerSupply.CoolerSize + " mm",
                GPUInputNumber = powerSupply.GPUInputNumber,
                SATAInputNumber = powerSupply.SATAInputNumber,
                Manufacturer = powerSupply.Manufacturer.Name,
                //PowerSupplyCPUInterfaces = powerSupply.PowerSupplyPowerSupplyCPUInterfaces.Select(m => new PowerSupplyCPUInterfaceViewModel { Id = m.PowerSupplyCPUInterface.Id, Name = m.PowerSupplyCPUInterface.Name }).ToList(),
                Price = powerSupply.Price,
                ImagePath = "/Images/PowerSupply/" + powerSupply.Image,
                Description = powerSupply.Description
            };

            return PartialView("PartialDetails", model);
        }

        // GET: PowerSupply/Create
        public ActionResult Create()
        {
            var powerSuppliesMotherboardInterfaces = _motherBoardPowerSupplyInterfaceService.GetPowerSupplyMotherBoardInterfaces();
            ViewBag.PowerSuppliesMotherboardInterfaces = new SelectList(powerSuppliesMotherboardInterfaces, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            var powerSupplyCPUInterfaces = _powerSupplyCPUInterfaceService.GetPowerSupplyCPUInterfaces();
            ViewBag.PowerSupplyCPUInterfaces = new SelectList(powerSupplyCPUInterfaces, "Id", "Name");
            return View();
        }

        // POST: PowerSupply/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(PowerSupplyViewModel model)
        {
            if (ModelState.IsValid)
            {
                //if(model.PowerSupplyCPUInterfaces == null)
                //{
                //    model.PowerSupplyCPUInterfaces = new List<PowerSupplyCPUInterfaceViewModel>();
                //}
                var helper = new ImageHelper(_webHostEnvironment);
                var image = helper.GetUploadedFile(model.Image, "PowerSupply");
                var powerSupply = new PowerSupply()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Power = model.Power,
                    PowerSupplyMotherBoardInterfaceId = model.PowerSupplyMotherBoardInterfaceId,
                    ManufacturerId = model.ManufacturerId,
                    CoolerSize = model.CoolerSize,
                    SATAInputNumber = model.SATAInputNumber,
                    GPUInputNumber = model.GPUInputNumber,
                    Image = image,
                    Price = model.Price
                };

                //powerSupply.PowerSupplyPowerSupplyCPUInterfaces = model.PowerSupplyCPUInterfaces.Select(m => new PowerSupplyPowerSupplyCPUInterface() { PowerSupplyCPUInterfaceId = m.Id }).ToList();
                var result = _powerSupplyService.CreatePowerSupply(powerSupply);

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "PowerSupply" });
                }

                return NotFound(result);
            }
            var powerSuppliesMotherboardInterfaces = _motherBoardPowerSupplyInterfaceService.GetPowerSupplyMotherBoardInterfaces();
            ViewBag.PowerSuppliesMotherboardInterfaces = new SelectList(powerSuppliesMotherboardInterfaces, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            var powerSupplyCPUInterfaces = _powerSupplyCPUInterfaceService.GetPowerSupplyCPUInterfaces();
            ViewBag.PowerSupplyCPUInterfaces = new SelectList(powerSupplyCPUInterfaces, "Id", "Name");
            return View(model);
        }


        // GET: PowerSupply/Create
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var powerSupply = _powerSupplyService.GetPowerSupply(id);
            if (powerSupply == null)
            {
                return NotFound();
            }

            var model = new PowerSupplyViewModel()
            {
                Name = powerSupply.Name,
                Description = powerSupply.Description,
                Power = powerSupply.Power,
                PowerSupplyMotherBoardInterfaceId = powerSupply.PowerSupplyMotherBoardInterfaceId,
                ManufacturerId = powerSupply.ManufacturerId,
                CoolerSize = powerSupply.CoolerSize,
                SATAInputNumber = powerSupply.SATAInputNumber,
                GPUInputNumber = powerSupply.GPUInputNumber,
                ImagePath = "/Images/PowerSupply/" + powerSupply.Image,
                Price = powerSupply.Price,
                //PowerSupplyCPUInterfaces = powerSupply.PowerSupplyPowerSupplyCPUInterfaces.Select(m => new PowerSupplyCPUInterfaceViewModel() { Id = m.PowerSupplyCPUInterface.Id, Name = m.PowerSupplyCPUInterface.Name }).ToList()
            };

            var powerSuppliesMotherboardInterfaces = _motherBoardPowerSupplyInterfaceService.GetPowerSupplyMotherBoardInterfaces();
            ViewBag.PowerSuppliesMotherboardInterfaces = new SelectList(powerSuppliesMotherboardInterfaces, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            var powerSupplyCPUInterfaces = _powerSupplyCPUInterfaceService.GetPowerSupplyCPUInterfaces();
            ViewBag.PowerSupplyCPUInterfaces = new SelectList(powerSupplyCPUInterfaces, "Id", "Name");
            return View("Edit", model);
        }

        // POST: PowerSupply/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(PowerSupplyViewModel model)
        {
            var powerSupply = _powerSupplyService.GetPowerSupply(model.Id);
            if (powerSupply == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //if (model.PowerSupplyCPUInterfaces == null)
                //{
                //    model.PowerSupplyCPUInterfaces = new List<PowerSupplyCPUInterfaceViewModel>();
                //}
                powerSupply.Name = model.Name;
                powerSupply.Description = model.Description;
                powerSupply.ManufacturerId = model.ManufacturerId;
                powerSupply.Price = model.Price;
                powerSupply.Power = model.Power;
                powerSupply.PowerSupplyMotherBoardInterfaceId = model.PowerSupplyMotherBoardInterfaceId;
                powerSupply.CoolerSize = model.CoolerSize;
                powerSupply.SATAInputNumber = model.SATAInputNumber;
                powerSupply.GPUInputNumber = model.GPUInputNumber;

                if (model.Image != null)
                {
                    var helper = new ImageHelper(_webHostEnvironment);
                    var image = helper.GetUploadedFile(model.Image, "PowerSupply");
                    powerSupply.Image = image;
                }
                //powerSupply.PowerSupplyPowerSupplyCPUInterfaces = model.PowerSupplyCPUInterfaces.Select(m => new PowerSupplyPowerSupplyCPUInterface() { PowerSupplyCPUInterfaceId = m.Id }).ToList();

                var result = _powerSupplyService.UpdatePowerSupply(powerSupply);

                model.Id = powerSupply.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "PowerSupply" });
                }

                return NotFound(result);
            }
            var powerSuppliesMotherboardInterfaces = _motherBoardPowerSupplyInterfaceService.GetPowerSupplyMotherBoardInterfaces();
            ViewBag.PowerSuppliesMotherboardInterfaces = new SelectList(powerSuppliesMotherboardInterfaces, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            var powerSupplyCPUInterfaces = _powerSupplyCPUInterfaceService.GetPowerSupplyCPUInterfaces();
            ViewBag.PowerSupplyCPUInterfaces = new SelectList(powerSupplyCPUInterfaces, "Id", "Name");
            return View("Edit", model);
        }


        // POST: PowerSupply/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var powerSupply = _powerSupplyService.GetPowerSupply(id);
                if (powerSupply == null)
                {
                    return NotFound();
                }

                var result = _powerSupplyService.DeletePowerSupply(id);
                return Json(result);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        private string CreateShortDescription(string description)
        {
            var sentences = description.Split('.');
            return (sentences != null && sentences[0] != null) ? sentences[0] + "..."  : "";
        }
    }
}