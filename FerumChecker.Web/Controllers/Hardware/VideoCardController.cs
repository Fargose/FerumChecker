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
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{
    [Authorize]

    public class VideoCardController : Controller
    {

        private readonly IVideoCardService _videoCardService;
        private readonly IGraphicMemoryTypeService _graphicMemoryTypeService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IGPUService _gpuService;
        private readonly IVideoCardInterfaceService _videoCardInterfaceService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IComputerAssemblyService _assemblyService;
        private readonly IUserService _userService;

        public VideoCardController(IVideoCardService videoCardService, IWebHostEnvironment hostEnvironment, IManufacturerService manufacturerService, IVideoCardInterfaceService videoCardInterfaceService, IGraphicMemoryTypeService graphicMemoryTypeService, IGPUService gpuService, IComputerAssemblyService assemblyService, IUserService userService)
        {
            _videoCardService = videoCardService;
            _webHostEnvironment = hostEnvironment;
            _videoCardInterfaceService = videoCardInterfaceService;
            _gpuService = gpuService;
            _graphicMemoryTypeService = graphicMemoryTypeService;
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
            var result = _assemblyService.SetVideoCard(id, assemblyId);
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
            var result = _assemblyService.RemoveVideoCard(computerAssembly);
            return Json(result);
        }
        public IActionResult SmallList()
        {
            var cpus = _videoCardService.GetVideoCards().OrderBy(m => m.Name);

            var model = cpus.Select(m => new VideoCardViewModel
            {
                Id = m.Id,
                Name = m.Name,
                ImagePath = "/Images/VideoCard/" + m.Image
            });

            return PartialView(model);
        }

        // GET: VideoCard
        public ActionResult Index()
        {
            var videoCards = _videoCardService.GetVideoCards().OrderBy(m => m.Name);

            var model = videoCards.Select(m => new VideoCardViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: VideoCard
        public ActionResult PartialIndex(VideoCardSearchModel searchDetails)
        {
            var videoCards = _videoCardService.GetVideoCards().OrderBy(m => m.Name);
            var model = videoCards.Select(m => new VideoCardViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                ShortDescription = string.IsNullOrEmpty(m.Description) ? "" : CreateShortDescription(m.Description),
                Frequency = m.Frequency,
                FrequencyDisplay = (m.Frequency) + " MHz",
                MemoryFrequencyDisplay = (m.MemoryFrequency) + " MHz",
                MemorySize = m.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(m.MemorySize),
                ManufacturerId = m.ManufacturerId,
                Manufacturer = m.Manufacturer.Name,
                ImagePath = "/Images/VideoCard/" + m.Image,
                GPU = m.GPU.Name,
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
                if (searchDetails.MinFrequency.HasValue)
                {
                    model = model.Where(m => m.Frequency >= searchDetails.MinFrequency);
                }
                if (searchDetails.MaxFrequency.HasValue)
                {
                    model = model.Where(m => m.Frequency <= searchDetails.MaxFrequency);
                }
                if (searchDetails.MinVideoMemory.HasValue)
                {
                    model = model.Where(m => m.MemorySize >= searchDetails.MinVideoMemory);
                }
                if (searchDetails.MaxVideoMemory.HasValue)
                {
                    model = model.Where(m => m.MemorySize <= searchDetails.MaxVideoMemory);
                }

            }

            return PartialView("Index", model);
        }

        // GET: VideoCard/Details/5
        public ActionResult Details(int id)
        {
            var videoCard = _videoCardService.GetVideoCard(id);
            if(videoCard == null)
            {
                return NotFound();
            }
            var model = new VideoCardViewModel()
            {
                Id = videoCard.Id,
                Name = videoCard.Name,
                Description = videoCard.Description,
                Frequency = videoCard.Frequency,
                FrequencyDisplay = (videoCard.Frequency) + " MHz",
                MemoryFrequencyDisplay = videoCard.MemoryFrequency + " MHz",
                MemorySize = videoCard.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(videoCard.MemorySize),
                MinimumPowerConsumingDisplay = videoCard.MinimumPowerConsuming + " Вт",
                ManufacturerId = videoCard.ManufacturerId,
                Manufacturer = videoCard.Manufacturer.Name,
                VideoCardInterfaceId = videoCard.VideoCardInterfaceId,
                VideoCardInterface = (new VideoCardInterfaceViewModel() { Name = videoCard.VideoCardInterface.Name, Multiplier = videoCard.VideoCardInterface.Multiplier, Version = videoCard.VideoCardInterface.Version }).FullName,
                GraphicMemoryTypeId = videoCard.GraphicMemoryTypeId,
                GraphicMemoryType = videoCard.GraphicMemoryType.Name,
                GPUId = videoCard.GPUId,
                GPU = videoCard.GPU.Name,
                ImagePath = "/Images/VideoCard/" + videoCard.Image,
                Price = videoCard.Price
            };

            return View(model);
        }


        public ActionResult PartialDetails(int id)
        {
            var videoCard = _videoCardService.GetVideoCard(id);
            if (videoCard == null)
            {
                return NotFound();
            }
            var model = new VideoCardViewModel()
            {
                Id = videoCard.Id,
                Name = videoCard.Name,
                Description = videoCard.Description,
                Frequency = videoCard.Frequency,
                FrequencyDisplay = (videoCard.Frequency) + " MHz",
                MemoryFrequencyDisplay = videoCard.MemoryFrequency + " MHz",
                MemorySize = videoCard.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(videoCard.MemorySize),
                MinimumPowerConsumingDisplay = videoCard.MinimumPowerConsuming + " Вт",
                ManufacturerId = videoCard.ManufacturerId,
                Manufacturer = videoCard.Manufacturer.Name,
                VideoCardInterfaceId = videoCard.VideoCardInterfaceId,
                VideoCardInterface = (new VideoCardInterfaceViewModel() { Name = videoCard.VideoCardInterface.Name, Multiplier = videoCard.VideoCardInterface.Multiplier, Version = videoCard.VideoCardInterface.Version }).FullName,
                GraphicMemoryTypeId = videoCard.GraphicMemoryTypeId,
                GraphicMemoryType = videoCard.GraphicMemoryType.Name,
                GPUId = videoCard.GPUId,
                GPU = videoCard.GPU.Name,
                ImagePath = "/Images/VideoCard/" + videoCard.Image,
                Price = videoCard.Price
            };

            return PartialView("PartialDetails", model);
        }

        [Authorize(Roles = "Administrator")]
        // GET: VideoCard/Create
        public ActionResult Create()
        {
            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().Select(m => new VideoCardInterfaceViewModel() { Id = m.Id, Name = m.Name, Multiplier = m.Multiplier, Version = m.Version });
            ViewBag.VideoCardInterfaces = new SelectList(videoCardInterfaces, "Id", "FullName");
            var gpus = _gpuService.GetGPUs();
            ViewBag.GPUs = new SelectList(gpus, "Id", "Name");
            var graphicMemoryTypes = _graphicMemoryTypeService.GetGraphicMemoryTypes();
            ViewBag.GraphicMemoryTypes = new SelectList(graphicMemoryTypes, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View();
        }

        // POST: VideoCard/Create
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(VideoCardViewModel model)
        {
            if (ModelState.IsValid)
            {
                var helper = new ImageHelper(_webHostEnvironment);
                var image = helper.GetUploadedFile(model.Image, "VideoCard");
                var videoCard = new VideoCard()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Frequency = model.Frequency,
                    MemoryFrequency = model.MemoryFrequency,
                    MemorySize = model.MemorySize,
                    MinimumPowerConsuming = model.MinimumPowerConsuming,
                    ManufacturerId = model.ManufacturerId,
                    VideoCardInterfaceId = model.VideoCardInterfaceId,
                    GraphicMemoryTypeId = model.GraphicMemoryTypeId,
                    GPUId = model.GPUId,
                    Price = model.Price,
                    Image = image
                };

                var result = _videoCardService.CreateVideoCard(videoCard);

                model.Id = videoCard.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "VideoCard" });
                }

                return NotFound(result);
            }
            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().Select(m => new VideoCardInterfaceViewModel() { Id = m.Id, Name = m.Name, Multiplier = m.Multiplier, Version = m.Version });
            ViewBag.VideoCardInterfaces = new SelectList(videoCardInterfaces, "Id", "FullName");
            var gpus = _gpuService.GetGPUs();
            ViewBag.GPUs = new SelectList(gpus, "Id", "Name");
            var graphicMemoryTypes = _graphicMemoryTypeService.GetGraphicMemoryTypes();
            ViewBag.GraphicMemoryTypes = new SelectList(graphicMemoryTypes, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View(model);
        }


        // GET: VideoCard/Create
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var videoCard = _videoCardService.GetVideoCard(id);
            if (videoCard == null)
            {
                return NotFound();
            }

            var model = new VideoCardViewModel()
            {
                Id = videoCard.Id,
                Name = videoCard.Name,
                Description = videoCard.Description,
                Frequency = videoCard.Frequency,
                FrequencyDisplay = (videoCard.Frequency) + " MHz",
                MemorySize = videoCard.MemorySize,
                MemorySizeDisplay = CreateMemoryDescription(videoCard.MemorySize),
                MinimumPowerConsumingDisplay = videoCard.MinimumPowerConsuming + " Вт",
                MinimumPowerConsuming = videoCard.MinimumPowerConsuming,
                MemoryFrequency = videoCard.MemoryFrequency,
                ManufacturerId = videoCard.ManufacturerId,
                Manufacturer = videoCard.Manufacturer.Name,
                VideoCardInterfaceId = videoCard.VideoCardInterfaceId,
                VideoCardInterface = (new VideoCardInterfaceViewModel() { Name = videoCard.VideoCardInterface.Name, Multiplier = videoCard.VideoCardInterface.Multiplier, Version = videoCard.VideoCardInterface.Version }).FullName,
                GraphicMemoryTypeId = videoCard.GraphicMemoryTypeId,
                GraphicMemoryType = videoCard.GraphicMemoryType.Name,
                GPUId = videoCard.GPUId,
                GPU = videoCard.GPU.Name,
                ImagePath = "/Images/VideoCard/" + videoCard.Image,
                Price = videoCard.Price
            };

            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().Select(m => new VideoCardInterfaceViewModel() { Id = m.Id, Name = m.Name, Multiplier = m.Multiplier, Version = m.Version });
            ViewBag.VideoCardInterfaces = new SelectList(videoCardInterfaces, "Id", "FullName");
            var gpus = _gpuService.GetGPUs();
            ViewBag.GPUs = new SelectList(gpus, "Id", "Name");
            var graphicMemoryTypes = _graphicMemoryTypeService.GetGraphicMemoryTypes();
            ViewBag.GraphicMemoryTypes = new SelectList(graphicMemoryTypes, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }

        // POST: VideoCard/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(VideoCardViewModel model)
        {
            var videoCard = _videoCardService.GetVideoCard(model.Id);
            if (videoCard == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                videoCard.Name = model.Name;
                videoCard.Description = model.Description;
                videoCard.Frequency = model.Frequency;
                videoCard.MemoryFrequency = model.MemoryFrequency;
                videoCard.MemorySize = model.MemorySize;
                videoCard.MinimumPowerConsuming = model.MinimumPowerConsuming;
                videoCard.ManufacturerId = model.ManufacturerId;
                videoCard.VideoCardInterfaceId = model.VideoCardInterfaceId;
                videoCard.GraphicMemoryTypeId = model.GraphicMemoryTypeId;
                videoCard.GPUId = model.GPUId;
                videoCard.Price = model.Price;
                    

                if (model.Image != null)
                {
                    var helper = new ImageHelper(_webHostEnvironment);
                    var image = helper.GetUploadedFile(model.Image, "VideoCard");
                    videoCard.Image = image;
                }

                var result = _videoCardService.UpdateVideoCard(videoCard);

                model.Id = videoCard.Id;
                model.Image = null;

                if (result.Succedeed)
                {
                    return View("../Catalog/Index", new { startView = "VideoCard" });
                }

                return NotFound(result);
            }
            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().Select(m => new VideoCardInterfaceViewModel() { Id = m.Id, Name = m.Name, Multiplier = m.Multiplier, Version = m.Version });
            ViewBag.VideoCardInterfaces = new SelectList(videoCardInterfaces, "Id", "FullName");
            var gpus = _gpuService.GetGPUs();
            ViewBag.GPUs = new SelectList(gpus, "Id", "Name");
            var graphicMemoryTypes = _graphicMemoryTypeService.GetGraphicMemoryTypes();
            ViewBag.GraphicMemoryTypes = new SelectList(graphicMemoryTypes, "Id", "Name");
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return View("Edit", model);
        }


        // POST: VideoCard/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var videoCard = _videoCardService.GetVideoCard(id);
                if (videoCard == null)
                {
                    return NotFound();
                }

                var result = _videoCardService.DeleteVideoCard(id);
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
                return memory / 1024 + "ГБ";
            }
            return memory + " МБ";
        }

        public IActionResult Search()
        {
            var manufacturers = _manufacturerService.GetManufacturers();
            ViewBag.Manufacturers = new SelectList(manufacturers, "Id", "Name");
            return PartialView();
        }
    }
}