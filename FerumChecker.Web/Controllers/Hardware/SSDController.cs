﻿using System;
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
    public class SSDController : Controller
    {

        private readonly ISSDService _ssdService;
        private readonly IOuterMemoryFormFactorService _outerMemoryFormFactorService;
        private readonly IOuterMemoryInterfaceService _outerMemoryInterfaceService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IComputerAssemblyService _assemblyService;
        private readonly IUserService _userService;
        public SSDController(ISSDService ssdService, IWebHostEnvironment hostEnvironment, IManufacturerService manufacturerService, IOuterMemoryInterfaceService outerMemoryInterfaceService, IOuterMemoryFormFactorService outerMemoryFormFactorService, IComputerAssemblyService assemblyService, IUserService userService)
        {
            _ssdService = ssdService;
            _webHostEnvironment = hostEnvironment;
            _outerMemoryInterfaceService = outerMemoryInterfaceService;
            _outerMemoryFormFactorService = outerMemoryFormFactorService;
            _manufacturerService = manufacturerService;
            _assemblyService = assemblyService;
            _userService = userService;
        }

        public IActionResult SmallList()
        {
            var cpus = _ssdService.GetSSDs().OrderBy(m => m.Name);

            var model = cpus.Select(m => new SSDViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ImagePath = "/Images/SSD/" + m.Image
            });

            return PartialView(model);
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
            var result = _assemblyService.SetSSD(id, assemblyId);
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
            var result = _assemblyService.RemoveSSD(computerAssembly, hardwareId);
            return Json(result);
        }

        // GET: SSD
        public ActionResult Index()
        {
            var ssds = _ssdService.GetSSDs().OrderBy(m => m.Name);

            var model = ssds.Select(m => new SSDViewModel
            {
                //Id = m.Id,
                //Name = m.Name
            });

            return View(model);
        }

        // GET: SSD
        public ActionResult PartialIndex(OuterMemorySearchModel searchDetails)
        {
            var ssds = _ssdService.GetSSDs().OrderBy(m => m.Name);
            var model = ssds.Select(m => new SSDViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                ShortDescription = string.IsNullOrEmpty(m.Description) ? "" : CreateShortDescription(m.Description),
                WriteSpeed = m.WriteSpeed,
                WriteSpeedDisplay = m.WriteSpeed + " МБ/c",
                ReadSpeed = m.ReadSpeed,
                ReadSpeedDisplay = m.ReadSpeed + " МБ/c",
                MemorySize = m.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(m.MemorySize),
                OuterMemoryFormFactorId = m.OuterMemoryFormFactorId,
                OuterMemoryFormFactor = m.OuterMemoryFormFactor.Name,
                OuterMemoryInterfaceId = m.OuterMemoryInterfaceId,
                OuterMemoryInterface = m.OuterMemoryInterface.Name,
                ManufacturerId = m.ManufacturerId,
                Manufacturer = m.Manufacturer.Name,
                ImagePath = "/Images/SSD/" + m.Image,
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

        // GET: SSD/Details/5
        public ActionResult Details(int id)
        {
            var ssd = _ssdService.GetSSD(id);
            if(ssd == null)
            {
                return NotFound();
            }
            var model = new SSDViewModel()
            {
                Id = ssd.Id,
                Name = ssd.Name,
                Description = ssd.Description,
                ShortDescription = string.IsNullOrEmpty(ssd.Description) ? "" : CreateShortDescription(ssd.Description),
                WriteSpeed = ssd.WriteSpeed,
                WriteSpeedDisplay = ssd.WriteSpeed + " МБ/c",
                ReadSpeed = ssd.ReadSpeed,
                ReadSpeedDisplay = ssd.ReadSpeed + " МБ",
                MemorySize = ssd.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(ssd.MemorySize),
                OuterMemoryFormFactorId = ssd.OuterMemoryFormFactorId,
                OuterMemoryFormFactor = ssd.OuterMemoryFormFactor.Name,
                OuterMemoryInterfaceId = ssd.OuterMemoryInterfaceId,
                OuterMemoryInterface = ssd.OuterMemoryInterface.Name,
                ManufacturerId = ssd.ManufacturerId,
                Manufacturer = ssd.Manufacturer.Name,
                ImagePath = "/Images/SSD/" + ssd.Image,
                Price = ssd.Price
            };

            return View(model);
        }


        public ActionResult PartialDetails(int id)
        {
            var ssd = _ssdService.GetSSD(id);
            if (ssd == null)
            {
                return NotFound();
            }
            var model = new SSDViewModel()
            {
                Id = ssd.Id,
                Name = ssd.Name,
                Description = ssd.Description,
                ShortDescription = string.IsNullOrEmpty(ssd.Description) ? "" : CreateShortDescription(ssd.Description),
                WriteSpeed = ssd.WriteSpeed,
                WriteSpeedDisplay = ssd.WriteSpeed + " МБ/c",
                ReadSpeed = ssd.ReadSpeed,
                ReadSpeedDisplay = ssd.ReadSpeed + " МБ",
                MemorySize = ssd.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(ssd.MemorySize),
                OuterMemoryFormFactorId = ssd.OuterMemoryFormFactorId,
                OuterMemoryFormFactor = ssd.OuterMemoryFormFactor.Name,
                OuterMemoryInterfaceId = ssd.OuterMemoryInterfaceId,
                OuterMemoryInterface = ssd.OuterMemoryInterface.Name,
                ManufacturerId = ssd.ManufacturerId,
                Manufacturer = ssd.Manufacturer.Name,
                ImagePath = "/Images/SSD/" + ssd.Image,
                Price = ssd.Price
            };

            return PartialView("PartialDetails", model);
        }

        // GET: SSD/Create
        [Authorize(Roles = "Administrator")]
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

        // POST: SSD/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(SSDViewModel model)
        {
            if (ModelState.IsValid)
            {
                var helper = new ImageHelper(_webHostEnvironment);
                var image = helper.GetUploadedFile(model.Image, "SSD");
                var ssd = new SSD()
                {
                    Name = model.Name,
                    OuterMemoryInterfaceId = model.OuterMemoryInterfaceId,
                    MemorySize = model.MemorySize,
                    Description = model.Description,
                    OuterMemoryFormFactorId = model.OuterMemoryFormFactorId,
                    ReadSpeed = model.ReadSpeed,
                    WriteSpeed = model.WriteSpeed,
                    ManufacturerId = model.ManufacturerId,
                    Price = model.Price,
                    Image = image
                };

                var result = _ssdService.CreateSSD(ssd);

                model.Id = ssd.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "SSD" });
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


        // GET: SSD/Create
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var ssd = _ssdService.GetSSD(id);
            if (ssd == null)
            {
                return NotFound();
            }

            var model = new SSDViewModel()
            {
                Id = ssd.Id,
                Name = ssd.Name,
                OuterMemoryInterfaceId = ssd.OuterMemoryInterfaceId,
                MemorySize = ssd.MemorySize,
                Description = ssd.Description,
                OuterMemoryFormFactorId = ssd.OuterMemoryFormFactorId,
                ReadSpeed = ssd.ReadSpeed,
                WriteSpeed = ssd.WriteSpeed,
                ManufacturerId = ssd.ManufacturerId,
                Price = ssd.Price,
                Manufacturer = ssd.Manufacturer.Name,
                ImagePath = "/Images/SSD/" + ssd.Image,
            };
            var outerMemoryInterfaces = _outerMemoryInterfaceService.GetOuterMemoryInterfaces();
            ViewBag.OuterMemoryInterfaces = new SelectList(outerMemoryInterfaces, "Id", "Name");
            var outerMemoryFormFactors = _outerMemoryFormFactorService.GetOuterMemoryFormFactors();
            ViewBag.OuterMemoryFormFactors = new SelectList(outerMemoryFormFactors, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }

        // POST: SSD/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(SSDViewModel model)
        {
            var ssd = _ssdService.GetSSD(model.Id);
            if (ssd == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ssd.Name = model.Name;
                ssd.OuterMemoryInterfaceId = model.OuterMemoryInterfaceId;
                ssd.MemorySize = model.MemorySize;
                ssd.Description = model.Description;
                ssd.OuterMemoryFormFactorId = model.OuterMemoryFormFactorId;
                ssd.ReadSpeed = model.ReadSpeed;
                ssd.WriteSpeed = model.WriteSpeed;
                ssd.ManufacturerId = model.ManufacturerId;
                ssd.Price = model.Price;

                if (model.Image != null)
                {
                    var helper = new ImageHelper(_webHostEnvironment);
                    var image = helper.GetUploadedFile(model.Image, "SSD");
                    ssd.Image = image;
                }

                var result = _ssdService.UpdateSSD(ssd);

                model.Id = ssd.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "SSD" });
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


        // POST: SSD/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var ssd = _ssdService.GetSSD(id);
                if (ssd == null)
                {
                    return NotFound();
                }

                var result = _ssdService.DeleteSSD(id);
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