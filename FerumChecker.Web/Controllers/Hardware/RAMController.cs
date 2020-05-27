using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Service.Interfaces.Infrastructure;
using FerumChecker.Service.Interfaces.User;
using FerumChecker.Web.Code;
using FerumChecker.Web.ViewModel.Hardware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{

    public class RAMController : Controller
    {

        private readonly IRAMService _ramService;
        private readonly IRAMTypeService _ramTypeService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IComputerAssemblyService _assemblyService;
        private readonly IUserService _userService;

        public RAMController(IRAMService ramService, IWebHostEnvironment hostEnvironment, IRAMTypeService ramTypeService, IManufacturerService manufacturerService, IComputerAssemblyService assemblyService, IUserService userService)
        {
            _ramService = ramService;
            _webHostEnvironment = hostEnvironment;
            _ramTypeService = ramTypeService;
            _manufacturerService = manufacturerService;
            _assemblyService = assemblyService;
            _userService = userService;
        }


        public ActionResult SetHardware(int id, int assemblyId)
        {
            var computerAssembly = _assemblyService.GetComputerAssembly(assemblyId);
            if (computerAssembly == null)
            {
                return NotFound();
            }
            var userId = _userService.GetApplicationUserManager().GetUserId(this.User);
            if (computerAssembly.OwnerId != userId)
            {
                return Forbid();
            }
            var result = _assemblyService.SetRAM(id, assemblyId);
            return Json(result);
        }

        public ActionResult RemoveHardware(int assemblyId, int hardwareId)
        {
            var computerAssembly = _assemblyService.GetComputerAssembly(assemblyId);
            if (computerAssembly == null)
            {
                return NotFound();
            }
            var userId = _userService.GetApplicationUserManager().GetUserId(this.User);
            if (computerAssembly.OwnerId != userId)
            {
                return Forbid();
            }
            var result = _assemblyService.RemoveRam(computerAssembly, hardwareId);
            return Json(result);
        }
        public IActionResult SmallList()
        {
            var cpus = _ramService.GetRAMs().OrderBy(m => m.Name);

            var model = cpus.Select(m => new RAMViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ImagePath = "/Images/RAM/" + m.Image
            });

            return PartialView(model);
        }

        // GET: RAM
        public ActionResult Index()
        {
            var rams = _ramService.GetRAMs().OrderBy(m => m.Name);

            var model = rams.Select(m => new RAMViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: RAM
        public ActionResult PartialIndex(string search = "")
        {
            var rams = _ramService.GetRAMs().OrderBy(m => m.Name);
            var model = rams.Select(m => new RAMViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                ShortDescription = string.IsNullOrEmpty(m.Description) ? "" : CreateShortDescription(m.Description),
                Frequency = m.Frequency,
                FrequencyDisplay = (m.Frequency) + " MHz",
                MemorySize = m.MemorySize,
                RAMTypeId = m.RAMTypeId,
                RAMType = m.RAMType.Name,
                ManufacturerId = m.ManufacturerId,
                Manufacturer = m.Manufacturer.Name,
                ImagePath = "/Images/RAM/" + m.Image,
                Price = m.Price
            }).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search));

            return PartialView("Index", model);
        }

        // GET: RAM/Details/5
        public ActionResult Details(int id)
        {
            var ram = _ramService.GetRAM(id);
            if(ram == null)
            {
                return NotFound();
            }
            var model = new RAMViewModel()
            {
                Id = ram.Id,
                Name = ram.Name,
                Description = ram.Description,
                Frequency = ram.Frequency,
                FrequencyDisplay = (ram.Frequency) + " MHz",
                MemorySize = ram.MemorySize,
                RAMTypeId = ram.RAMTypeId,
                RAMType = ram.RAMType.Name,
                ManufacturerId = ram.ManufacturerId,
                Manufacturer = ram.Manufacturer.Name,
                ImagePath = "/Images/RAM/" + ram.Image,
                Price = ram.Price
            };

            return View(model);
        }


        public ActionResult PartialDetails(int id)
        {
            var ram = _ramService.GetRAM(id);
            if (ram == null)
            {
                return NotFound();
            }
            var model = new RAMViewModel()
            {
                Id = ram.Id,
                Name = ram.Name,
                Description = ram.Description,
                Frequency = ram.Frequency,
                FrequencyDisplay = (ram.Frequency) + " MHz",
                MemorySize = ram.MemorySize,
                RAMTypeId = ram.RAMTypeId,
                RAMType = ram.RAMType.Name,
                ManufacturerId = ram.ManufacturerId,
                Manufacturer = ram.Manufacturer.Name,
                ImagePath = "/Images/RAM/" + ram.Image,
                Price = ram.Price
            };

            return PartialView("PartialDetails", model);
        }
        // GET: RAM/Create
        public ActionResult Create()
        {
            var ramTypes = _ramTypeService.GetRAMTypes();
            ViewBag.RAMTypes = new SelectList(ramTypes, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View();
        }

        // POST: RAM/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(RAMViewModel model)
        {
            if (ModelState.IsValid)
            {
                var helper = new ImageHelper(_webHostEnvironment);
                var image = helper.GetUploadedFile(model.Image, "RAM");
                var ram = new RAM()
                {
                    Name = model.Name,
                    RAMTypeId = model.RAMTypeId,
                    MemorySize = model.MemorySize,
                    Description = model.Description,
                    Frequency = model.Frequency,
                    ManufacturerId = model.ManufacturerId,
                    Price = model.Price,
                    Image = image
                };

                var result = _ramService.CreateRAM(ram);

                model.Id = ram.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "RAM" });
                }

                return NotFound(result);
            }
            var ramTypes = _ramTypeService.GetRAMTypes();
            ViewBag.RAMTypes = new SelectList(ramTypes, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View(model);
        }


        // GET: RAM/Create
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ram = _ramService.GetRAM(id);
            if (ram == null)
            {
                return NotFound();
            }

            var model = new RAMViewModel()
            {
                Id = ram.Id,
                Name = ram.Name,
                Description = ram.Description,
                Frequency = ram.Frequency,
                FrequencyDisplay = (ram.Frequency) + " MHz",
                MemorySize = ram.MemorySize,
                RAMTypeId = ram.RAMTypeId,
                RAMType = ram.RAMType.Name,
                ManufacturerId = ram.ManufacturerId,
                Manufacturer = ram.Manufacturer.Name,
                ImagePath = "/Images/RAM/" + ram.Image,
                Price = ram.Price
            };

            var ramTypes = _ramTypeService.GetRAMTypes();
            ViewBag.RAMTypes = new SelectList(ramTypes, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }

        // POST: RAM/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(RAMViewModel model)
        {
            var ram = _ramService.GetRAM(model.Id);
            if (ram == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ram.Name = model.Name;
                ram.MemorySize = model.MemorySize;
                ram.RAMTypeId = model.RAMTypeId;
                ram.Description = model.Description;
                ram.Frequency = model.Frequency;
                ram.ManufacturerId = model.ManufacturerId;
                ram.Price = model.Price;

                if (model.Image != null)
                {
                    var helper = new ImageHelper(_webHostEnvironment);
                    var image = helper.GetUploadedFile(model.Image, "RAM");
                    ram.Image = image;
                }

                var result = _ramService.UpdateRAM(ram);

                model.Id = ram.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "RAM" });
                }

                return NotFound(result);
            }
            var ramTypes = _ramTypeService.GetRAMTypes();
            ViewBag.RAMTypes = new SelectList(ramTypes, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }


        // POST: RAM/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var ram = _ramService.GetRAM(id);
                if (ram == null)
                {
                    return NotFound();
                }

                var result = _ramService.DeleteRAM(id);
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