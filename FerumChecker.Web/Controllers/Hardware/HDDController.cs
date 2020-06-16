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
using FerumChecker.Web.ViewModel.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{
    [Authorize]
    public class HDDController : Controller
    {

        private readonly IHDDService _hddService;
        private readonly IOuterMemoryFormFactorService _outerMemoryFormFactorService;
        private readonly IOuterMemoryInterfaceService _outerMemoryInterfaceService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IComputerAssemblyService _assemblyService;
        private readonly IUserService _userService;
        public HDDController(IHDDService hddService, IWebHostEnvironment hostEnvironment, IManufacturerService manufacturerService, IOuterMemoryInterfaceService outerMemoryInterfaceService, IOuterMemoryFormFactorService outerMemoryFormFactorService, IComputerAssemblyService assemblyService, IUserService userService)
        {
            _hddService = hddService;
            _webHostEnvironment = hostEnvironment;
            _outerMemoryInterfaceService = outerMemoryInterfaceService;
            _outerMemoryFormFactorService = outerMemoryFormFactorService;
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
            var result = _assemblyService.SetHDD(id, assemblyId);
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
            var result = _assemblyService.RemoveHDD(computerAssembly, hardwareId);
            return Json(result);
        }
        public IActionResult SmallList()
        {
            var cpus = _hddService.GetHDDs().OrderBy(m => m.Name);

            var model = cpus.Select(m => new HDDViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ImagePath = "/Images/HDD/" + m.Image
            });

            return PartialView(model);
        }

        // GET: HDD
        public ActionResult Index()
        {
            var hdds = _hddService.GetHDDs().OrderBy(m => m.Name);

            var model = hdds.Select(m => new HDDViewModel
            {
                //Id = m.Id,
                //Name = m.Name
            });

            return View(model);
        }

        // GET: HDD
        public ActionResult PartialIndex(OuterMemorySearchModel searchDetails)
        {
            var hdds = _hddService.GetHDDs().OrderBy(m => m.Name);
            var model = hdds.Select(m => new HDDViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                ShortDescription = string.IsNullOrEmpty(m.Description) ? "" : CreateShortDescription(m.Description),
                DataTransferSpeed = m.DataTransferSpeed,
                DataTransferSpeedDisplay = m.DataTransferSpeed + " МБ/c",
                BufferSize = m.BufferSize,
                BufferSizeDisplay = m.BufferSize + " МБ",
                MemorySize = m.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(m.MemorySize),
                OuterMemoryFormFactorId = m.OuterMemoryFormFactorId,
                OuterMemoryFormFactor = m.OuterMemoryFormFactor.Name,
                OuterMemoryInterfaceId = m.OuterMemoryInterfaceId,
                OuterMemoryInterface = m.OuterMemoryInterface.Name,
                ManufacturerId = m.ManufacturerId,
                Manufacturer = m.Manufacturer.Name,
                ImagePath = "/Images/HDD/" + m.Image,
                Price = m.Price
            });

            if (searchDetails != null)
            {
                model = model.Where(m => string.IsNullOrEmpty(searchDetails.Name) || m.Name.Contains(searchDetails.Name));
                model = model.Where(m => m.ManufacturerId == searchDetails.ManufacturerId || searchDetails.ManufacturerId == null);
                if (searchDetails.MinPrice.HasValue)
                {
                    model = model.Where(m => m.Price >= searchDetails.MinPrice);
                }
                if (searchDetails.MaxPrice.HasValue)
                {
                    model = model.Where(m => m.Price <= searchDetails.MaxPrice);
                }
                if (searchDetails.MinVolume.HasValue)
                {
                    model = model.Where(m => m.MemorySize >= searchDetails.MinVolume);
                }
                if (searchDetails.MaxVolume.HasValue)
                {
                    model = model.Where(m => m.MemorySize <= searchDetails.MaxVolume);
                }

            }

            return PartialView("Index", model);
        }

        // GET: HDD/Details/5
        public ActionResult Details(int id)
        {
            var hdd = _hddService.GetHDD(id);
            if(hdd == null)
            {
                return NotFound();
            }
            var model = new HDDViewModel()
            {
                Id = hdd.Id,
                Name = hdd.Name,
                Description = hdd.Description,
                ShortDescription = string.IsNullOrEmpty(hdd.Description) ? "" : CreateShortDescription(hdd.Description),
                DataTransferSpeed = hdd.DataTransferSpeed,
                DataTransferSpeedDisplay = hdd.DataTransferSpeed + " МБ/c",
                BufferSize = hdd.BufferSize,
                BufferSizeDisplay = hdd.BufferSize + " МБ",
                MemorySize = hdd.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(hdd.MemorySize),
                OuterMemoryFormFactorId = hdd.OuterMemoryFormFactorId,
                OuterMemoryFormFactor = hdd.OuterMemoryFormFactor.Name,
                OuterMemoryInterfaceId = hdd.OuterMemoryInterfaceId,
                OuterMemoryInterface = hdd.OuterMemoryInterface.Name,
                ManufacturerId = hdd.ManufacturerId,
                Manufacturer = hdd.Manufacturer.Name,
                ImagePath = "/Images/HDD/" + hdd.Image,
                Price = hdd.Price
            };

            return View(model);
        }


        // GET: HDD/Details/5
        public ActionResult PartialDetails(int id)
        {
            var hdd = _hddService.GetHDD(id);
            if (hdd == null)
            {
                return NotFound();
            }
            var model = new HDDViewModel()
            {
                Id = hdd.Id,
                Name = hdd.Name,
                Description = hdd.Description,
                ShortDescription = string.IsNullOrEmpty(hdd.Description) ? "" : CreateShortDescription(hdd.Description),
                DataTransferSpeed = hdd.DataTransferSpeed,
                DataTransferSpeedDisplay = hdd.DataTransferSpeed + " МБ/c",
                BufferSize = hdd.BufferSize,
                BufferSizeDisplay = hdd.BufferSize + " МБ",
                MemorySize = hdd.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(hdd.MemorySize),
                OuterMemoryFormFactorId = hdd.OuterMemoryFormFactorId,
                OuterMemoryFormFactor = hdd.OuterMemoryFormFactor.Name,
                OuterMemoryInterfaceId = hdd.OuterMemoryInterfaceId,
                OuterMemoryInterface = hdd.OuterMemoryInterface.Name,
                ManufacturerId = hdd.ManufacturerId,
                Manufacturer = hdd.Manufacturer.Name,
                ImagePath = "/Images/HDD/" + hdd.Image,
                Price = hdd.Price
            };

            return PartialView("PartialDetails", model);
        }

        [Authorize(Roles = "Administrator")]
        // GET: HDD/Create
        public ActionResult Create()
        {
            var outerMemoryInterfaces = _outerMemoryInterfaceService.GetOuterMemoryInterfaces();
            ViewBag.OuterMemoryInterfaces = new SelectList(outerMemoryInterfaces, "Id", "Name");
            var outerMemoryFormFactors = _outerMemoryFormFactorService.GetOuterMemoryFormFactors();
            ViewBag.OuterMemoryFormFactors = new SelectList(outerMemoryFormFactors, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View();
        }

        // POST: HDD/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(HDDViewModel model)
        {
            if (ModelState.IsValid)
            {
                var helper = new ImageHelper(_webHostEnvironment);
                var image = helper.GetUploadedFile(model.Image, "HDD");
                var hdd = new HDD()
                {
                    Name = model.Name,
                    OuterMemoryInterfaceId = model.OuterMemoryInterfaceId,
                    MemorySize = model.MemorySize,
                    Description = model.Description,
                    OuterMemoryFormFactorId = model.OuterMemoryFormFactorId,
                    BufferSize = model.BufferSize,
                    DataTransferSpeed = model.DataTransferSpeed,
                    ManufacturerId = model.ManufacturerId,
                    Price = model.Price,
                    Image = image
                };

                var result = _hddService.CreateHDD(hdd);

                model.Id = hdd.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "HDD" });
                }

                return NotFound(result);
            }
            var outerMemoryInterfaces = _outerMemoryInterfaceService.GetOuterMemoryInterfaces();
            ViewBag.OuterMemoryInterfaces = new SelectList(outerMemoryInterfaces, "Id", "Name");
            var outerMemoryFormFactors = _outerMemoryFormFactorService.GetOuterMemoryFormFactors();
            ViewBag.OuterMemoryFormFactors = new SelectList(outerMemoryFormFactors, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View(model);
        }


        // GET: HDD/Create
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var hdd = _hddService.GetHDD(id);
            if (hdd == null)
            {
                return NotFound();
            }

            var model = new HDDViewModel()
            {
                Id = hdd.Id,
                Name = hdd.Name,
                OuterMemoryInterfaceId = hdd.OuterMemoryInterfaceId,
                MemorySize = hdd.MemorySize,
                Description = hdd.Description,
                OuterMemoryFormFactorId = hdd.OuterMemoryFormFactorId,
                BufferSize = hdd.BufferSize,
                DataTransferSpeed = hdd.DataTransferSpeed,
                ManufacturerId = hdd.ManufacturerId,
                Price = hdd.Price,
                Manufacturer = hdd.Manufacturer.Name,
                ImagePath = "/Images/HDD/" + hdd.Image,
            };
            var outerMemoryInterfaces = _outerMemoryInterfaceService.GetOuterMemoryInterfaces();
            ViewBag.OuterMemoryInterfaces = new SelectList(outerMemoryInterfaces, "Id", "Name");
            var outerMemoryFormFactors = _outerMemoryFormFactorService.GetOuterMemoryFormFactors();
            ViewBag.OuterMemoryFormFactors = new SelectList(outerMemoryFormFactors, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }

        // POST: HDD/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(HDDViewModel model)
        {
            var hdd = _hddService.GetHDD(model.Id);
            if (hdd == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                hdd.Name = model.Name;
                hdd.OuterMemoryInterfaceId = model.OuterMemoryInterfaceId;
                hdd.MemorySize = model.MemorySize;
                hdd.Description = model.Description;
                hdd.OuterMemoryFormFactorId = model.OuterMemoryFormFactorId;
                hdd.BufferSize = model.BufferSize;
                hdd.DataTransferSpeed = model.DataTransferSpeed;
                hdd.ManufacturerId = model.ManufacturerId;
                hdd.Price = model.Price;

                if (model.Image != null)
                {
                    var helper = new ImageHelper(_webHostEnvironment);
                    var image = helper.GetUploadedFile(model.Image, "HDD");
                    hdd.Image = image;
                }

                var result = _hddService.UpdateHDD(hdd);

                model.Id = hdd.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "HDD" });
                }

                return NotFound(result);
            }
            var outerMemoryInterfaces = _outerMemoryInterfaceService.GetOuterMemoryInterfaces();
            ViewBag.OuterMemoryInterfaces = new SelectList(outerMemoryInterfaces, "Id", "Name");
            var outerMemoryFormFactors = _outerMemoryFormFactorService.GetOuterMemoryFormFactors();
            ViewBag.OuterMemoryFormFactors = new SelectList(outerMemoryFormFactors, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }


        // POST: HDD/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var hdd = _hddService.GetHDD(id);
                if (hdd == null)
                {
                    return NotFound();
                }

                var result = _hddService.DeleteHDD(id);
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

        private string CreateMemoryDescription(int memory)
        {
            if (memory > 1024)
            {
                return memory / 1024 + "TБ";
            }
            return memory + " ГБ";
        }

        public IActionResult Search()
        {
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");

            return PartialView();
        }
    }
}