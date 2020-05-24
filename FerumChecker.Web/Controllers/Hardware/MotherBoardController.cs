﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Web.Code;
using FerumChecker.Web.ViewModel.Hardware;
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{

    public class MotherBoardController : Controller
    {

        private readonly IMotherBoardService _motherBoardService;
        private readonly ICPUSocketService _cpuSocketService;
        private readonly IMotherBoardNorthBridgeService _northBridgeService;
        private readonly IMotherBoardFormFactorService _motherBoardFormFactorService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOuterMemoryInterfaceService _outerMemoryInterfaceService;
        private readonly IVideoCardInterfaceService _videoCardInterfaceService;
        private readonly IPowerSupplyMotherBoardInterfaceService _powerSupplyMotherBoardInterfaceService;
        private readonly IRAMTypeService _ramTypeService;
        public MotherBoardController(IMotherBoardService motherBoardService, IWebHostEnvironment hostEnvironment, ICPUSocketService cpuSocketService, IManufacturerService manufacturerService, IMotherBoardNorthBridgeService northBridgeService, IMotherBoardFormFactorService motherBoardFormFactorService, IOuterMemoryInterfaceService outerMemoryInterfaceService, IVideoCardInterfaceService videoCardInterfaceService, IPowerSupplyMotherBoardInterfaceService powerSupplyMotherBoardInterfaceService, IRAMTypeService ramTypeService)
        {
            _motherBoardService = motherBoardService;
            _webHostEnvironment = hostEnvironment;
            _cpuSocketService = cpuSocketService;
            _manufacturerService = manufacturerService;
            _northBridgeService = northBridgeService;
            _motherBoardFormFactorService = motherBoardFormFactorService;
            _outerMemoryInterfaceService = outerMemoryInterfaceService;
            _videoCardInterfaceService = videoCardInterfaceService;
            _powerSupplyMotherBoardInterfaceService = powerSupplyMotherBoardInterfaceService;
            _ramTypeService = ramTypeService;

        }
        // GET: MotherBoard
        public ActionResult Index()
        {
            var motherBoards = _motherBoardService.GetMotherBoards().OrderBy(m => m.Name);

            var model = motherBoards.Select(m => new MotherBoardViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: MotherBoard
        public ActionResult PartialIndex(string search = "")
        {
            var motherBoards = _motherBoardService.GetMotherBoards().OrderBy(m => m.Name);
            var model = motherBoards.Select(m => new MotherBoardViewModel
            {
                Id = m.Id,
                Name = m.Name,
                //CoresName = m.CoresName,
                //CoresNumber = m.CoresNumber,
                //Description = m.Description,
                ShortDescription = string.IsNullOrEmpty(m.Description) ? "" : CreateShortDescription(m.Description),
                //Frequency = m.Frequency,
                //FrequencyDisplay = ((double)m.Frequency/1000) + " GHz",
                //MaxFrequency = m.MaxFrequency,
                //MaxFrequencyDisplay = ((double)m.MaxFrequency / 1000) + " GHz",
                //MotherBoardSocket = m.MotherBoardSocket.Name,
                //MotherBoardSocketId = m.MotherBoardSocketId,
                //ManufacturerId = m.ManufacturerId,
                Manufacturer = m.Manufacturer.Name,
                MaxMemory = m.MaxMemory,
                CPUSocket = m.CPUSocket.Name,
                ImagePath = "/Images/MotherBoard/" + m.Image,
                //ThreadsNumber = m.ThreadsNumber,
                Price = m.Price
            }).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search));

            return PartialView("Index", model);
        }

        // GET: MotherBoard/Details/5
        public ActionResult Details(int id)
        {
            var motherBoard = _motherBoardService.GetMotherBoard(id);
            if(motherBoard == null)
            {
                return NotFound();
            }
            var model = new MotherBoardViewModel()
            {
                Id = motherBoard.Id,
                Name = motherBoard.Name,
                MaxMemory = motherBoard.MaxMemory,
                CPUSocket = motherBoard.CPUSocket.Name,
                MotherBoardNothernBridge = motherBoard.MotherBoardNothernBridge.Name,
                MotherBoardFormFactor = motherBoard.MotherBoardFormFactor.Name,
                Manufacturer = motherBoard.Manufacturer.Name,
                VideoCardInterfaces = motherBoard.MotherBoardVideoCardSlots.Select(m => new VideoCardInterfaceViewModel { Id = m.VideoCardInterface.Id, Name = m.VideoCardInterface.Name, Multiplier = m.VideoCardInterface.Multiplier, Version = m.VideoCardInterface.Version }).ToList(),
                MotherBoardRAMSlots = motherBoard.MotherBoardRAMSlots.Select(m => new RAMTypeViewModel { Id = m.RAMType.Id, Name = m.RAMType.Name }).ToList(),
                PowerSupplyMotherBoardInterfaces = motherBoard.PowerSupplyMotherBoardSlots.Select(m => new PowerSupplyMotherBoardInterfaceViewModel { Id = m.PowerSupplyMotherBoardInterface.Id, Name = m.PowerSupplyMotherBoardInterface.Name }).ToList(),
                OuterMemoryInterfaces = motherBoard.MotherBoardOuterMemorySlots.Select(m => new OuterMemoryInterfaceViewModel { Id = m.OuterMemoryInterface.Id, Name = m.OuterMemoryInterface.Name }).ToList(),
                Price = motherBoard.Price,
                ImagePath = "/Images/MotherBoard/" + motherBoard.Image
            };

            return View(model);
        }

        // GET: MotherBoard/Create
        public ActionResult Create()
        {
            var sockets = _cpuSocketService.GetCPUSockets();
            ViewBag.Sockets = new SelectList(sockets, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            var motherBoardFormFactors = _motherBoardFormFactorService.GetMotherBoardFormFactors();
            ViewBag.MotherBoardFormFactors = new SelectList(motherBoardFormFactors, "Id", "Name");
            var motherBoardNothernBridges = _northBridgeService.GetMotherBoardNorthBridges();
            ViewBag.MotherBoardNothernBridges = new SelectList(motherBoardNothernBridges, "Id", "Name");
            var outerMemoryInterfaces = _outerMemoryInterfaceService.GetOuterMemoryInterfaces();
            ViewBag.OuterMemoryInterfaces = new SelectList(outerMemoryInterfaces, "Id", "Name");
            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().Select(m => new VideoCardInterfaceViewModel() { Id = m.Id, Name = m.Name, Multiplier = m.Multiplier, Version = m.Version });
            ViewBag.VideoCardInterfaces = new SelectList(videoCardInterfaces, "Id", "FullName");
            var powerSupplyMotherBoardInterfaces = _powerSupplyMotherBoardInterfaceService.GetPowerSupplyMotherBoardInterfaces();
            ViewBag.PowerSupplyMotherBoardInterfaces = new SelectList(powerSupplyMotherBoardInterfaces, "Id", "Name");
            var ramTypes = _ramTypeService.GetRAMTypes();
            ViewBag.RamTypes = new SelectList(ramTypes, "Id", "Name");
            return View();
        }

        // POST: MotherBoard/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(MotherBoardViewModel model)
        {
            if (ModelState.IsValid)
            {
                var helper = new ImageHelper(_webHostEnvironment);
                var image = helper.GetUploadedFile(model.Image, "MotherBoard");
                var motherBoard = new MotherBoard()
                {
                    Name = model.Name,
                    Description = model.Description,
                    MaxMemory = model.MaxMemory,
                    CPUSocketId = model.CPUSocketId,
                    ManufacturerId = model.ManufacturerId,
                    MotherBoardNothernBridgeId = model.MotherBoardNothernBridgeId,
                    MotherBoardFormFactorId = model.MotherBoardFormFactorId,
                    Image = image
                };

                motherBoard.MotherBoardOuterMemorySlots = model.OuterMemoryInterfaces.Select(m => new MotherBoardOuterMemorySlot() { OuterMemoryInterfaceId = m.Id }).ToList();
                motherBoard.MotherBoardVideoCardSlots = model.VideoCardInterfaces.Select(m => new MotherBoardVideoCardSlot() { VideoCardInterfaceId = m.Id.Value }).ToList();
                motherBoard.MotherBoardRAMSlots = model.MotherBoardRAMSlots.Select(m => new MotherBoardRAMSlot() { RAMTypeId = m.Id }).ToList();
                motherBoard.PowerSupplyMotherBoardSlots = model.PowerSupplyMotherBoardInterfaces.Select(m => new MotherBoardPowerSupplySlot() { PowerSupplyMotherBoardInterfaceId = m.Id }).ToList();
                var result = _motherBoardService.CreateMotherBoard(motherBoard);

                //model.Id = motherBoard.Id;
                //model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "MotherBoard" });
                }

                return NotFound(result);
            }
            var sockets = _cpuSocketService.GetCPUSockets();
            ViewBag.Sockets = new SelectList(sockets, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            var motherBoardFormFactors = _motherBoardFormFactorService.GetMotherBoardFormFactors();
            ViewBag.MotherBoardFormFactors = new SelectList(motherBoardFormFactors, "Id", "Name");
            var motherBoardNothernBridges = _northBridgeService.GetMotherBoardNorthBridges();
            ViewBag.MotherBoardNothernBridges = new SelectList(motherBoardNothernBridges, "Id", "Name");
            var outerMemoryInterfaces = _outerMemoryInterfaceService.GetOuterMemoryInterfaces();
            ViewBag.OuterMemoryInterfaces = new SelectList(outerMemoryInterfaces, "Id", "Name");
            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().Select(m => new VideoCardInterfaceViewModel() { Id = m.Id, Name = m.Name, Multiplier = m.Multiplier, Version = m.Version });
            ViewBag.VideoCardInterfaces = new SelectList(videoCardInterfaces, "Id", "Name");
            var powerSupplyMotherBoardInterfaces = _powerSupplyMotherBoardInterfaceService.GetPowerSupplyMotherBoardInterfaces();
            ViewBag.PowerSupplyMotherBoardInterfaces = new SelectList(powerSupplyMotherBoardInterfaces, "Id", "Name");
            var ramTypes = _ramTypeService.GetRAMTypes();
            ViewBag.RamTypes = new SelectList(ramTypes, "Id", "Name");
            return View(model);
        }


        // GET: MotherBoard/Create
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var motherBoard = _motherBoardService.GetMotherBoard(id);
            if (motherBoard == null)
            {
                return NotFound();
            }

            var model = new MotherBoardViewModel()
            {
                //Id = motherBoard.Id,
                //Name = motherBoard.Name,
                //CoresName = motherBoard.CoresName,
                //CoresNumber = motherBoard.CoresNumber,
                //Description = motherBoard.Description,
                //Frequency = motherBoard.Frequency,
                //MaxFrequency = motherBoard.MaxFrequency,
                //MotherBoardSocketId = motherBoard.MotherBoardSocketId,
                //ManufacturerId = motherBoard.ManufacturerId,
                //ThreadsNumber = motherBoard.ThreadsNumber,
                //Price = motherBoard.Price,
                //ImagePath = "/Images/MotherBoard/" + motherBoard.Image
            };

            var sockets = _cpuSocketService.GetCPUSockets();
            ViewBag.Sockets = new SelectList(sockets, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            var motherBoardFormFactors = _motherBoardFormFactorService.GetMotherBoardFormFactors();
            ViewBag.MotherBoardFormFactors = new SelectList(motherBoardFormFactors, "Id", "Name");
            var motherBoardNothernBridges = _northBridgeService.GetMotherBoardNorthBridges();
            ViewBag.MotherBoardNothernBridges = new SelectList(motherBoardNothernBridges, "Id", "Name");
            var outerMemoryInterfaces = _outerMemoryInterfaceService.GetOuterMemoryInterfaces();
            ViewBag.OuterMemoryInterfaces = new SelectList(outerMemoryInterfaces, "Id", "Name");
            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().Select(m => new VideoCardInterfaceViewModel() { Id = m.Id, Name = m.Name, Multiplier = m.Multiplier, Version = m.Version });
            ViewBag.VideoCardInterfaces = new SelectList(videoCardInterfaces, "Id", "Name");
            var powerSupplyMotherBoardInterfaces = _powerSupplyMotherBoardInterfaceService.GetPowerSupplyMotherBoardInterfaces();
            ViewBag.PowerSupplyMotherBoardInterface = new SelectList(powerSupplyMotherBoardInterfaces, "Id", "Name");
            var ramTypes = _ramTypeService.GetRAMTypes();
            ViewBag.RamTypes = new SelectList(ramTypes, "Id", "Name");
            return View("Edit", model);
        }

        // POST: MotherBoard/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(MotherBoardViewModel model)
        {
            var motherBoard = _motherBoardService.GetMotherBoard(model.Id);
            if (motherBoard == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //motherBoard.Name = model.Name;
                //motherBoard.CoresName = model.CoresName;
                //motherBoard.CoresNumber = model.CoresNumber;
                //motherBoard.Description = model.Description;
                //motherBoard.Frequency = model.Frequency;
                //motherBoard.MaxFrequency = model.MaxFrequency;
                //motherBoard.MotherBoardSocketId = model.MotherBoardSocketId;
                //motherBoard.ManufacturerId = model.ManufacturerId;
                //motherBoard.ThreadsNumber = model.ThreadsNumber;
                motherBoard.Price = model.Price;

                if (model.Image != null)
                {
                    var helper = new ImageHelper(_webHostEnvironment);
                    var image = helper.GetUploadedFile(model.Image, "MotherBoard");
                    motherBoard.Image = image;
                }

                var result = _motherBoardService.UpdateMotherBoard(motherBoard);

                model.Id = motherBoard.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "MotherBoard" });
                }

                return NotFound(result);
            }
            var sockets = _cpuSocketService.GetCPUSockets();
            ViewBag.Sockets = new SelectList(sockets, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            var motherBoardFormFactors = _motherBoardFormFactorService.GetMotherBoardFormFactors();
            ViewBag.MotherBoardFormFactors = new SelectList(motherBoardFormFactors, "Id", "Name");
            var motherBoardNothernBridges = _northBridgeService.GetMotherBoardNorthBridges();
            ViewBag.MotherBoardNothernBridges = new SelectList(motherBoardNothernBridges, "Id", "Name");
            var outerMemoryInterfaces = _outerMemoryInterfaceService.GetOuterMemoryInterfaces();
            ViewBag.OuterMemoryInterfaces = new SelectList(outerMemoryInterfaces, "Id", "Name");
            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().Select(m => new VideoCardInterfaceViewModel() { Id = m.Id, Name = m.Name, Multiplier = m.Multiplier, Version = m.Version });
            ViewBag.VideoCardInterfaces = new SelectList(videoCardInterfaces, "Id", "Name");
            var powerSupplyMotherBoardInterfaces = _powerSupplyMotherBoardInterfaceService.GetPowerSupplyMotherBoardInterfaces();
            ViewBag.PowerSupplyMotherBoardInterface = new SelectList(powerSupplyMotherBoardInterfaces, "Id", "Name");
            var ramTypes = _ramTypeService.GetRAMTypes();
            ViewBag.RamTypes = new SelectList(ramTypes, "Id", "Name");
            return View("Edit", model);
        }


        // POST: MotherBoard/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var motherBoard = _motherBoardService.GetMotherBoard(id);
                if (motherBoard == null)
                {
                    return NotFound();
                }

                var result = _motherBoardService.DeleteMotherBoard(id);
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