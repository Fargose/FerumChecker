using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Web.Code;
using FerumChecker.Web.ViewModel.Hardware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{

    public class SSDController : Controller
    {

        private readonly ISSDService _ssdService;
        private readonly IOuterMemoryFormFactorService _outerMemoryFormFactorService;
        private readonly IOuterMemoryInterfaceService _outerMemoryInterfaceService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SSDController(ISSDService ssdService, IWebHostEnvironment hostEnvironment, IManufacturerService manufacturerService, IOuterMemoryInterfaceService outerMemoryInterfaceService, IOuterMemoryFormFactorService outerMemoryFormFactorService)
        {
            _ssdService = ssdService;
            _webHostEnvironment = hostEnvironment;
            _outerMemoryInterfaceService = outerMemoryInterfaceService;
            _outerMemoryFormFactorService = outerMemoryFormFactorService;
            _manufacturerService = manufacturerService;
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
        public ActionResult PartialIndex(string search = "")
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
            }).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search));

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
    }
}