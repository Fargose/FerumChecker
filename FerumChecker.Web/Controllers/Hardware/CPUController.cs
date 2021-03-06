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
    public class CPUController : Controller
    {

        private readonly ICPUService _cpuService;
        private readonly ICPUSocketService _cpuSocketService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IComputerAssemblyService _assemblyService;
        private readonly IUserService _userService; 
        public CPUController(ICPUService cpuService, IWebHostEnvironment hostEnvironment, ICPUSocketService cpuSocketService, IManufacturerService manufacturerService, IComputerAssemblyService assemblyService, IUserService userService)
        {
            _cpuService = cpuService;
            _webHostEnvironment = hostEnvironment;
            _cpuSocketService = cpuSocketService;
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
            var result = _assemblyService.SetCPU(id, assemblyId);
            return Json(result);
        }

        public ActionResult RemoveHardware(int assemblyId)
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
            var result = _assemblyService.RemoveCPU(computerAssembly);
            return Json(result);
        }

        // GET: CPU
        public ActionResult Index()
        {
            var cpus = _cpuService.GetCPUs().OrderBy(m => m.Name);

            var model = cpus.Select(m => new CPUViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: CPU
        public ActionResult PartialIndex(CPUSearchModel searchDetails = null)
        {
            var cpus = _cpuService.GetCPUs().OrderBy(m => m.Name);
            var model = cpus.Select(m => new CPUViewModel
            {
                Id = m.Id,
                Name = m.Name,
                CoresName = m.CoresName,
                CoresNumber = m.CoresNumber,
                Description = m.Description,
                ShortDescription = string.IsNullOrEmpty(m.Description) ? "" : CreateShortDescription(m.Description),
                Frequency = m.Frequency,
                FrequencyDisplay = ((double)m.Frequency / 1000) + " GHz",
                MaxFrequency = m.MaxFrequency,
                MaxFrequencyDisplay = ((double)m.MaxFrequency / 1000) + " GHz",
                CPUSocket = m.CPUSocket.Name,
                CPUSocketId = m.CPUSocketId,
                ManufacturerId = m.ManufacturerId,
                Manufacturer = m.Manufacturer.Name,
                ImagePath = "/Images/CPU/" + m.Image,
                ThreadsNumber = m.ThreadsNumber,
                Price = m.Price
            });
            if(searchDetails != null){
                model = model.Where(m => string.IsNullOrEmpty(searchDetails.Name) || m.Name.Contains(searchDetails.Name));
                model = model.Where(m => m.CPUSocketId == searchDetails.CPUSocketId || searchDetails.CPUSocketId == null);
                model = model.Where(m => m.ManufacturerId == searchDetails.ManufacturerId || searchDetails.ManufacturerId == null);
                if (searchDetails.MinPrice.HasValue)
                {
                    model = model.Where(m => m.Price >= searchDetails.MinPrice);
                }
                if (searchDetails.MaxPrice.HasValue)
                {
                    model = model.Where(m => m.Price <= searchDetails.MaxPrice);
                }
                if (searchDetails.MinFrequency.HasValue)
                {
                    model = model.Where(m => m.Frequency >= searchDetails.MinFrequency);
                }
                if (searchDetails.MaxFrequency.HasValue)
                {
                    model = model.Where(m => m.Frequency <= searchDetails.MaxFrequency);
                }
                if (searchDetails.MinCores.HasValue)
                {
                    model = model.Where(m => m.CoresNumber >= searchDetails.MinCores);
                }
                if (searchDetails.MaxCores.HasValue)
                {
                    model = model.Where(m => m.CoresNumber <= searchDetails.MaxCores);
                }

            }

            return PartialView("Index", model);
        }

        // GET: CPU/Details/5
        public ActionResult Details(int id)
        {
            var cpu = _cpuService.GetCPU(id);
            if(cpu == null)
            {
                return NotFound();
            }
            var model = new CPUViewModel()
            {
                Id = cpu.Id,
                Name = cpu.Name,
                CoresName = cpu.CoresName,
                CoresNumber = cpu.CoresNumber,
                Description = cpu.Description,
                Frequency = cpu.Frequency,
                MaxFrequency = cpu.MaxFrequency,
                CPUSocketId = cpu.CPUSocketId,
                CPUSocket = cpu.CPUSocket.Name,
                ManufacturerId = cpu.ManufacturerId,
                Manufacturer = cpu.Manufacturer.Name,
                FrequencyDisplay = ((double)cpu.Frequency / 1000) + " GHz",
                MaxFrequencyDisplay = ((double)cpu.MaxFrequency / 1000) + " GHz",
                ThreadsNumber = cpu.ThreadsNumber,
                Price = cpu.Price,
                ImagePath = "/Images/CPU/" + cpu.Image
            };

            return View(model);
        }

        public ActionResult PartialDetails(int id)
        {
            var cpu = _cpuService.GetCPU(id);
            if (cpu == null)
            {
                return NotFound();
            }
            var model = new CPUViewModel()
            {
                Id = cpu.Id,
                Name = cpu.Name,
                CoresName = cpu.CoresName,
                CoresNumber = cpu.CoresNumber,
                Description = cpu.Description,
                Frequency = cpu.Frequency,
                MaxFrequency = cpu.MaxFrequency,
                CPUSocketId = cpu.CPUSocketId,
                CPUSocket = cpu.CPUSocket.Name,
                ManufacturerId = cpu.ManufacturerId,
                Manufacturer = cpu.Manufacturer.Name,
                FrequencyDisplay = ((double)cpu.Frequency / 1000) + " GHz",
                MaxFrequencyDisplay = ((double)cpu.MaxFrequency / 1000) + " GHz",
                ThreadsNumber = cpu.ThreadsNumber,
                Price = cpu.Price,
                ImagePath = "/Images/CPU/" + cpu.Image
            };

            return PartialView("PartialDetails", model);
        }

        public IActionResult SmallList()
        {
            var cpus = _cpuService.GetCPUs().OrderBy(m => m.Name);

            var model = cpus.Select(m => new CPUViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ImagePath = "/Images/CPU/" + m.Image
            });

            return PartialView(model);
        }

        // GET: CPU/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var sockets = _cpuSocketService.GetCPUSockets();
            ViewBag.Sockets = new SelectList(sockets, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View();
        }

        // POST: CPU/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(CPUViewModel model)
        {
            if (ModelState.IsValid)
            {
                var helper = new ImageHelper(_webHostEnvironment);
                var image = helper.GetUploadedFile(model.Image, "CPU");
                var cpu = new CPU()
                {
                    Name = model.Name,
                    CoresName = model.CoresName,
                    CoresNumber = model.CoresNumber,
                    Description = model.Description,
                    Frequency = model.Frequency,
                    MaxFrequency = model.MaxFrequency,
                    CPUSocketId = model.CPUSocketId,
                    ManufacturerId = model.ManufacturerId,
                    ThreadsNumber = model.ThreadsNumber,
                    Price = model.Price,
                    Image = image
                };

                var result = _cpuService.CreateCPU(cpu);

                model.Id = cpu.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "CPU" });
                }

                return NotFound(result);
            }
            var sockets = _cpuSocketService.GetCPUSockets();
            ViewBag.Sockets = new SelectList(sockets, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View(model);
        }


        // GET: CPU/Create
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var cpu = _cpuService.GetCPU(id);
            if (cpu == null)
            {
                return NotFound();
            }

            var model = new CPUViewModel()
            {
                Id = cpu.Id,
                Name = cpu.Name,
                CoresName = cpu.CoresName,
                CoresNumber = cpu.CoresNumber,
                Description = cpu.Description,
                Frequency = cpu.Frequency,
                MaxFrequency = cpu.MaxFrequency,
                CPUSocketId = cpu.CPUSocketId,
                ManufacturerId = cpu.ManufacturerId,
                ThreadsNumber = cpu.ThreadsNumber,
                Price = cpu.Price,
                ImagePath = "/Images/CPU/" + cpu.Image
            };

            var sockets = _cpuSocketService.GetCPUSockets();
            ViewBag.Sockets = new SelectList(sockets, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }

        // POST: CPU/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(CPUViewModel model)
        {
            var cpu = _cpuService.GetCPU(model.Id);
            if (cpu == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                cpu.Name = model.Name;
                cpu.CoresName = model.CoresName;
                cpu.CoresNumber = model.CoresNumber;
                cpu.Description = model.Description;
                cpu.Frequency = model.Frequency;
                cpu.MaxFrequency = model.MaxFrequency;
                cpu.CPUSocketId = model.CPUSocketId;
                cpu.ManufacturerId = model.ManufacturerId;
                cpu.ThreadsNumber = model.ThreadsNumber;
                cpu.Price = model.Price;

                if (model.Image != null)
                {
                    var helper = new ImageHelper(_webHostEnvironment);
                    var image = helper.GetUploadedFile(model.Image, "CPU");
                    cpu.Image = image;
                }

                var result = _cpuService.UpdateCPU(cpu);

                model.Id = cpu.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "CPU" });
                }

                return NotFound(result);
            }
            var sockets = _cpuSocketService.GetCPUSockets();
            ViewBag.Sockets = new SelectList(sockets, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }


        // POST: CPU/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var cpu = _cpuService.GetCPU(id);
                if (cpu == null)
                {
                    return NotFound();
                }

                var result = _cpuService.DeleteCPU(id);
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

        public IActionResult Search()
        {
            var sockets = _cpuSocketService.GetCPUSockets();
            ViewBag.Sockets = new SelectList(sockets, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return PartialView();
        }
    }
}