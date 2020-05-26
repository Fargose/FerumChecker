using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Web.Code;
using FerumChecker.Web.ViewModel.Hardware;
using FerumChecker.Web.ViewModel.Infrastructure;
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{

    public class SoftwareController : Controller
    {

        private readonly ISoftwareService _softwareService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDeveloperService _developerService;
        private readonly IPublisherService _publisherService;
        private readonly ICPUService _cpuService;
        private readonly IVideoCardService _videoCardService;

        public SoftwareController(ISoftwareService powerSupplyService, IWebHostEnvironment hostEnvironment, IPublisherService publisherService, IDeveloperService developerService, ICPUService cpuService, IVideoCardService videoCardService)
        {
            _softwareService = powerSupplyService;
            _webHostEnvironment = hostEnvironment;
            _publisherService = publisherService;
            _developerService = developerService;
            _cpuService = cpuService;
            _videoCardService = videoCardService;
        }
        // GET: Software
        public ActionResult Index()
        {
            var powerSupplys = _softwareService.GetSoftwares().OrderBy(m => m.Name);

            var model = powerSupplys.Select(m => new SoftwareViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: Software
        public ActionResult PartialIndex(string search = "")
        {
            var powerSupplys = _softwareService.GetSoftwares().OrderBy(m => m.Name);
            var model = powerSupplys.Select(m => new SoftwareViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ShortDescription = string.IsNullOrEmpty(m.Description) ? "" : CreateShortDescription(m.Description),
                Publisher = m.Publisher.Name,
                Developer = m.Developer.Name,
                RecomendedCPURequirmentsDisplay = CreateCPuDisplay(m.SoftwareCPURequirements.Where(m => m.RequirementTypeId == 2).ToList()),
                RecomendedVideoCardRequirmentsDisplay = CreateVideoCardDisplay(m.SoftwareVideoCardRequirements.Where(m => m.RequirementTypeId == 2).ToList()),
                MinimiumRequiredRAMDisplay = m.MinimiumRequiredRAM + " Гб",
                RecommendedRequiredRAMDisplay = m.RecommendedRequiredRAM + " Гб",
                DiscVolumeDisplay = CreateMemoryDescription(m.DiscVolume),
                MinimumCPURequirmentsDisplay = CreateCPuDisplay(m.SoftwareCPURequirements.Where(m => m.RequirementTypeId == 1).ToList()),
                MinimumVideoCardRequirmentsDisplay = CreateVideoCardDisplay(m.SoftwareVideoCardRequirements.Where(m => m.RequirementTypeId == 1).ToList()),
                ImagePath = "/Images/Software/" + m.Image,
                Price = m.Price
            }).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search));

            return PartialView("Index", model);
        }

        // GET: Software/Details/5
        public ActionResult Details(int id)
        {
            var powerSupply = _softwareService.GetSoftware(id);
            if(powerSupply == null)
            {
                return NotFound();
            }
            var model = new SoftwareViewModel()
            {
                Id = powerSupply.Id,
                Name = powerSupply.Name,
                ShortDescription = string.IsNullOrEmpty(powerSupply.Description) ? "" : CreateShortDescription(powerSupply.Description),
                Publisher = powerSupply.Publisher.Name,
                Developer = powerSupply.Developer.Name,
                RecomendedCPURequirmentsDisplay = CreateCPuDisplay(powerSupply.SoftwareCPURequirements.Where(m => m.RequirementTypeId == 2).ToList()),
                RecomendedVideoCardRequirmentsDisplay = CreateVideoCardDisplay(powerSupply.SoftwareVideoCardRequirements.Where(m => m.RequirementTypeId == 2).ToList()),
                MinimiumRequiredRAMDisplay = powerSupply.MinimiumRequiredRAM + " Гб",
                RecommendedRequiredRAMDisplay = powerSupply.RecommendedRequiredRAM + " Гб",
                DiscVolumeDisplay = CreateMemoryDescription(powerSupply.DiscVolume),
                MinimumCPURequirmentsDisplay = CreateCPuDisplay(powerSupply.SoftwareCPURequirements.Where(m => m.RequirementTypeId == 1).ToList()),
                MinimumVideoCardRequirmentsDisplay = CreateVideoCardDisplay(powerSupply.SoftwareVideoCardRequirements.Where(m => m.RequirementTypeId == 1).ToList()),
                ImagePath = "/Images/Software/" + powerSupply.Image,
                Price = powerSupply.Price,
                Description = powerSupply.Description
            };

            return View(model);
        }

        // GET: Software/Create
        public ActionResult Create()
        {
            var cpus = _cpuService.GetCPUs();
            ViewBag.CPUs = new SelectList(cpus, "Id", "Name");
            var videoCards = _videoCardService.GetVideoCards();
            ViewBag.VideoCards = new SelectList(videoCards, "Id", "Name");
            var publishers = _publisherService.GetPublishers();
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name");
            var developers = _developerService.GetDevelopers();
            ViewBag.Developers = new SelectList(developers, "Id", "Name");
            return View();
        }

        // POST: Software/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(SoftwareViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.SoftwareCPURequirements == null)
                {
                    model.SoftwareCPURequirements = new List<SoftwareCPURequirement>();
                }
                if (model.SoftwareVideoCardRequirements == null)
                {
                    model.SoftwareVideoCardRequirements = new List<SoftwareVideoCardRequirement>();
                }
                var helper = new ImageHelper(_webHostEnvironment);
                var image = helper.GetUploadedFile(model.Image, "Software");
                var powerSupply = new Software()
                {
                    Name = model.Name,
                    Description = model.Description,
                    DiscVolume = model.DiscVolume,
                    MinimiumRequiredRAM = model.MinimiumRequiredRAM,
                    RecommendedRequiredRAM = model.RecommendedRequiredRAM,
                    PublisherId = model.PublisherId,
                    DeveloperId = model.DeveloperId,
                    Image = image,
                    Price = model.Price
                };

                powerSupply.SoftwareCPURequirements = model.SoftwareCPURequirements.Select(m => new SoftwareCPURequirement() { CPUId = m.CPUId, RequirementTypeId = m.RequirementTypeId }).ToList();
                powerSupply.SoftwareVideoCardRequirements = model.SoftwareVideoCardRequirements.Select(m => new SoftwareVideoCardRequirement() { VideoCardId = m.VideoCardId, RequirementTypeId = m.RequirementTypeId }).ToList();
                
                var result = _softwareService.CreateSoftware(powerSupply);

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "Software" });
                }

                return NotFound(result);
            }
            var cpus = _cpuService.GetCPUs();
            ViewBag.CPUs = new SelectList(cpus, "Id", "Name");
            var videoCards = _videoCardService.GetVideoCards();
            ViewBag.VideoCards = new SelectList(videoCards, "Id", "Name");
            var publishers = _publisherService.GetPublishers();
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name");
            var developers = _developerService.GetDevelopers();
            ViewBag.Developers = new SelectList(developers, "Id", "Name");
            return View(model);
        }


        // GET: Software/Create
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var powerSupply = _softwareService.GetSoftware(id);
            if (powerSupply == null)
            {
                return NotFound();
            }

            var model = new SoftwareViewModel()
            {
                Name = powerSupply.Name,
                Description = powerSupply.Description,
                ImagePath = "/Images/Software/" + powerSupply.Image,
                Price = powerSupply.Price,
                RecommendedRequiredRAM = powerSupply.RecommendedRequiredRAM,
                MinimiumRequiredRAM = powerSupply.MinimiumRequiredRAM,
                DiscVolume = powerSupply.DiscVolume,
                PublisherId = powerSupply.PublisherId,
                DeveloperId = powerSupply.DeveloperId,
                SoftwareCPURequirements = powerSupply.SoftwareCPURequirements,
                SoftwareVideoCardRequirements = powerSupply.SoftwareVideoCardRequirements
            };

            var cpus = _cpuService.GetCPUs();
            ViewBag.CPUs = new SelectList(cpus, "Id", "Name");
            var videoCards = _videoCardService.GetVideoCards();
            ViewBag.VideoCards = new SelectList(videoCards, "Id", "Name");
            var publishers = _publisherService.GetPublishers();
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name");
            var developers = _developerService.GetDevelopers();
            ViewBag.Developers = new SelectList(developers, "Id", "Name");
            return View("Edit", model);
        }

        // POST: Software/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(SoftwareViewModel model)
        {
            var software = _softwareService.GetSoftware(model.Id);
            if (software == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (model.SoftwareCPURequirements == null)
                {
                    model.SoftwareCPURequirements = new List<SoftwareCPURequirement>();
                }
                if (model.SoftwareVideoCardRequirements == null)
                {
                    model.SoftwareVideoCardRequirements = new List<SoftwareVideoCardRequirement>();
                }
                software.Name = model.Name;
                software.Description = model.Description;
                software.MinimiumRequiredRAM = model.MinimiumRequiredRAM;
                software.RecommendedRequiredRAM = model.RecommendedRequiredRAM;
                software.Price = model.Price;
                software.DiscVolume = model.DiscVolume;
                software.PublisherId = model.PublisherId;
                software.DeveloperId = model.DeveloperId;

                if (model.Image != null)
                {
                    var helper = new ImageHelper(_webHostEnvironment);
                    var image = helper.GetUploadedFile(model.Image, "Software");
                    software.Image = image;
                }
                software.SoftwareCPURequirements = model.SoftwareCPURequirements.Select(m => new SoftwareCPURequirement() { CPUId = m.CPUId, RequirementTypeId = m.RequirementTypeId }).ToList();
                software.SoftwareVideoCardRequirements = model.SoftwareVideoCardRequirements.Select(m => new SoftwareVideoCardRequirement() { VideoCardId = m.VideoCardId, RequirementTypeId = m.RequirementTypeId }).ToList();

                var result = _softwareService.UpdateSoftware(software);

                model.Id = software.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "Software" });
                }

                return NotFound(result);
            }
            var cpus = _cpuService.GetCPUs();
            ViewBag.CPUs = new SelectList(cpus, "Id", "Name");
            var videoCards = _videoCardService.GetVideoCards();
            ViewBag.VideoCards = new SelectList(videoCards, "Id", "Name");
            var publishers = _publisherService.GetPublishers();
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name");
            var developers = _developerService.GetDevelopers();
            ViewBag.Developers = new SelectList(developers, "Id", "Name");
            return View("Edit", model);
        }


        // POST: Software/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var powerSupply = _softwareService.GetSoftware(id);
                if (powerSupply == null)
                {
                    return NotFound();
                }

                var result = _softwareService.DeleteSoftware(id);
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
                return memory / 1024 + "Гб";
            }
            return memory + " Мб";
        }

        public string CreateCPuDisplay(List<SoftwareCPURequirement> cpus)
        {
            var stringBuilder = new StringBuilder();
            foreach(var cpu in cpus)
            {
                stringBuilder.Append(cpu.CPU.Name);
                stringBuilder.Append(";");
            }

            return stringBuilder.ToString();
        
        }


        public string CreateVideoCardDisplay(List<SoftwareVideoCardRequirement> videocards)
        {
            var stringBuilder = new StringBuilder();
            foreach (var videocard in videocards)
            {
                stringBuilder.Append(videocard.VideoCard.Name);
                stringBuilder.Append(";");
            }

            return stringBuilder.ToString();

        }
    }
}