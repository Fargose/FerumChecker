﻿using System;
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
using FerumChecker.Web.Code;
using FerumChecker.Web.ViewModel.Hardware;
using FerumChecker.Web.ViewModel.Search;
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{
    [Authorize]
    public class PCCaseController : Controller
    {

        private readonly IPCCaseService _pcCaseService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMotherBoardFormFactorService _motherBoardFormFactorService;
        private readonly IOuterMemoryFormFactorService _outerMemoryFormFactorService;
        private readonly IComputerAssemblyService _computerAssemblyService;
        private readonly IUserService _userService;

        public PCCaseController(IPCCaseService pcCaseService, IWebHostEnvironment hostEnvironment, IManufacturerService manufacturerService, IMotherBoardFormFactorService motherBoardFormFactorService, IOuterMemoryFormFactorService outerMemoryFormFactorService, IComputerAssemblyService computerAssemblyService, IUserService userService)
        {
            _pcCaseService = pcCaseService;
            _webHostEnvironment = hostEnvironment;
            _manufacturerService = manufacturerService;
            _motherBoardFormFactorService = motherBoardFormFactorService;
            _outerMemoryFormFactorService = outerMemoryFormFactorService;
            _computerAssemblyService = computerAssemblyService;
            _userService = userService;
        }
        // GET: PCCase


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
            var result = _computerAssemblyService.SetPCCase(id, assemblyId);
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
            var result = _computerAssemblyService.RemovePCCase(computerAssembly);
            return Json(result);
        }
        public IActionResult SmallList()
        {
            var cpus = _pcCaseService.GetPCCases().OrderBy(m => m.Name);

            var model = cpus.Select(m => new PCCaseViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ImagePath = "/Images/PCCase/" + m.Image
            });

            return PartialView(model);
        }
        public ActionResult Index()
        {
            var pcCases = _pcCaseService.GetPCCases().OrderBy(m => m.Name);

            var model = pcCases.Select(m => new PCCaseViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: PCCase
        public ActionResult PartialIndex(PCCaseSearchModel searchDetails)
        {
            var pcCases = _pcCaseService.GetPCCases().OrderBy(m => m.Name);
            var model = pcCases.Select(m => new PCCaseViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ShortDescription = string.IsNullOrEmpty(m.Description) ? "" : CreateShortDescription(m.Description),
                Manufacturer = m.Manufacturer.Name,
                ManufacturerId = m.ManufacturerId,
                WeightDisplay = CreateWeightDescription(m.Weight),
                ImagePath = "/Images/PCCase/" + m.Image,
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
            }

            return PartialView("Index", model);
        }

        // GET: PCCase/Details/5
        public ActionResult Details(int id)
        {
            var pcCase = _pcCaseService.GetPCCase(id);
            if (pcCase == null)
            {
                return NotFound();
            }
            var model = new PCCaseViewModel()
            {
                Id = pcCase.Id,
                Name = pcCase.Name,
                WeightDisplay = CreateWeightDescription(pcCase.Weight),
                PCCaseMotherBoardFormFactors = pcCase.PCCaseMotherBoardFormFactors.Select(m => new MotherBoardFormFactorViewModel() { Id = m.MotherBoardFormFactor.Id, Name = m.MotherBoardFormFactor.Name }).ToList(),
                PCCaseOuterMemoryFormFactors = pcCase.PCCaseOuterMemoryFormFactors.Select(m => new OuterMemoryFormFactorViewModel() { Id = m.OuterMemoryFormFactors.Id, Name = m.OuterMemoryFormFactors.Name }).ToList(),
                Manufacturer = pcCase.Manufacturer.Name,         
                Price = pcCase.Price,
                ImagePath = "/Images/PCCase/" + pcCase.Image,
                Description = pcCase.Description
            };

            return View(model);
        }


        public ActionResult PartialDetails(int id)
        {
            var pcCase = _pcCaseService.GetPCCase(id);
            if (pcCase == null)
            {
                return NotFound();
            }
            var model = new PCCaseViewModel()
            {
                Id = pcCase.Id,
                Name = pcCase.Name,
                WeightDisplay = CreateWeightDescription(pcCase.Weight),
                PCCaseMotherBoardFormFactors = pcCase.PCCaseMotherBoardFormFactors.Select(m => new MotherBoardFormFactorViewModel() { Id = m.MotherBoardFormFactor.Id, Name = m.MotherBoardFormFactor.Name }).ToList(),
                PCCaseOuterMemoryFormFactors = pcCase.PCCaseOuterMemoryFormFactors.Select(m => new OuterMemoryFormFactorViewModel() { Id = m.OuterMemoryFormFactors.Id, Name = m.OuterMemoryFormFactors.Name }).ToList(),
                Manufacturer = pcCase.Manufacturer.Name,
                Price = pcCase.Price,
                ImagePath = "/Images/PCCase/" + pcCase.Image,
                Description = pcCase.Description
            };

            return PartialView("PartialDetails", model);
        }

        [Authorize(Roles = "Administrator")]
        // GET: PCCase/Create
        public ActionResult Create()
        {
            var motherBoardFormFactorService = _motherBoardFormFactorService.GetMotherBoardFormFactors();
            ViewBag.MotherBoardFormFactors = new SelectList(motherBoardFormFactorService, "Id", "Name");
            var outerMemoryFormFactors = _outerMemoryFormFactorService.GetOuterMemoryFormFactors();
            ViewBag.OuterMemoryFormFactors = new SelectList(outerMemoryFormFactors, "Id", "Name");
            var manufacturers  = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View();
        }

        // POST: PCCase/Create
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(PCCaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.PCCaseOuterMemoryFormFactors == null)
                {
                    model.PCCaseOuterMemoryFormFactors = new List<OuterMemoryFormFactorViewModel>();
                }
                if (model.PCCaseMotherBoardFormFactors == null)
                {
                    model.PCCaseMotherBoardFormFactors = new List<MotherBoardFormFactorViewModel>();
                }
                var helper = new ImageHelper(_webHostEnvironment);
                var image = helper.GetUploadedFile(model.Image, "PCCase");
                var pcCase = new PCCase()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Weight = model.Weight,
                    ManufacturerId = model.ManufacturerId,
                    Image = image,
                    Price = model.Price
                };

                pcCase.PCCaseMotherBoardFormFactors = model.PCCaseMotherBoardFormFactors.Select(m => new PCCaseMotherBoardFormFactor() { MotherBoardFormFactorId = m.Id }).ToList();
                pcCase.PCCaseOuterMemoryFormFactors = model.PCCaseOuterMemoryFormFactors.Select(m => new PCCaseOuterMemoryFormFactor() { OuterMemoryFormFactorId = m.Id }).ToList();
                var result = _pcCaseService.CreatePCCase(pcCase);

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "PCCase" });
                }

                return NotFound(result);
            }
            var motherBoardFormFactorService = _motherBoardFormFactorService.GetMotherBoardFormFactors();
            ViewBag.MotherBoardFormFactors = new SelectList(motherBoardFormFactorService, "Id", "Name");
            var outerMemoryFormFactors = _outerMemoryFormFactorService.GetOuterMemoryFormFactors();
            ViewBag.OuterMemoryFormFactors = new SelectList(outerMemoryFormFactors, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View(model);
        }


        // GET: PCCase/Create
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var pcCase = _pcCaseService.GetPCCase(id);
            if (pcCase == null)
            {
                return NotFound();
            }

            var model = new PCCaseViewModel()
            {

                Name = pcCase.Name,
                Description = pcCase.Description,
                ManufacturerId = pcCase.ManufacturerId,
                Weight = pcCase.Weight,
                Price = pcCase.Price,
                PCCaseMotherBoardFormFactors = pcCase.PCCaseMotherBoardFormFactors.Select(m => new MotherBoardFormFactorViewModel() { Id = m.MotherBoardFormFactor.Id, Name = m.MotherBoardFormFactor.Name }).ToList(),
                PCCaseOuterMemoryFormFactors = pcCase.PCCaseOuterMemoryFormFactors.Select(m => new OuterMemoryFormFactorViewModel() { Id = m.OuterMemoryFormFactors.Id, Name = m.OuterMemoryFormFactors.Name }).ToList(),
                ImagePath = "/Images/PCCase/" + pcCase.Image    
            };

            var motherBoardFormFactorService = _motherBoardFormFactorService.GetMotherBoardFormFactors();
            ViewBag.MotherBoardFormFactors= new SelectList(motherBoardFormFactorService, "Id", "Name");
            var outerMemoryFormFactors = _outerMemoryFormFactorService.GetOuterMemoryFormFactors();
            ViewBag.OuterMemoryFormFactors = new SelectList(outerMemoryFormFactors, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }

        // POST: PCCase/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(PCCaseViewModel model)
        {
            var pcCase = _pcCaseService.GetPCCase(model.Id);
            if (pcCase == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (model.PCCaseOuterMemoryFormFactors == null)
                {
                    model.PCCaseOuterMemoryFormFactors = new List<OuterMemoryFormFactorViewModel>();
                }
                if (model.PCCaseMotherBoardFormFactors == null)
                {
                    model.PCCaseMotherBoardFormFactors = new List<MotherBoardFormFactorViewModel>();
                }
                pcCase.Name = model.Name;
                pcCase.Description = model.Description;
                pcCase.ManufacturerId = model.ManufacturerId;
                pcCase.Price = model.Price;
                pcCase.Weight = model.Weight;

                if (model.Image != null)
                {
                    var helper = new ImageHelper(_webHostEnvironment);
                    var image = helper.GetUploadedFile(model.Image, "PCCase");
                    pcCase.Image = image;
                }
                pcCase.PCCaseMotherBoardFormFactors = model.PCCaseMotherBoardFormFactors.Select(m => new PCCaseMotherBoardFormFactor() { MotherBoardFormFactorId = m.Id }).ToList();
                pcCase.PCCaseOuterMemoryFormFactors = model.PCCaseOuterMemoryFormFactors.Select(m => new PCCaseOuterMemoryFormFactor() { OuterMemoryFormFactorId = m.Id }).ToList();

                var result = _pcCaseService.UpdatePCCase(pcCase);

                model.Id = pcCase.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "PCCase" });
                }

                return NotFound(result);
            }
            var motherBoardFormFactorService = _motherBoardFormFactorService.GetMotherBoardFormFactors();
            ViewBag.MotherBoardFormFactors = new SelectList(motherBoardFormFactorService, "Id", "Name");
            var outerMemoryFormFactors = _outerMemoryFormFactorService.GetOuterMemoryFormFactors();
            ViewBag.OuterMemoryFormFactors = new SelectList(outerMemoryFormFactors, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }


        // POST: PCCase/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var pcCase = _pcCaseService.GetPCCase(id);
                if (pcCase == null)
                {
                    return NotFound();
                }

                var result = _pcCaseService.DeletePCCase(id);
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


        private string CreateWeightDescription(int weight)
        {
            if (weight > 1000)
            {
                return (double)(weight) / 1000 + "Кг";
            }
            return weight + " г";
        }

        public IActionResult Search()
        {
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            var motherBoardFormFactors = _motherBoardFormFactorService.GetMotherBoardFormFactors();
            ViewBag.MotherBoardFormFactors = new SelectList(motherBoardFormFactors, "Id", "Name");

            return PartialView();
        }
    }
}