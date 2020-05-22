using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FerumChecker.Web.Controllers
{

    public class VideoCardInterfaceController : Controller
    {

        private readonly IVideoCardInterfaceService _videoCardInterfaceService;
        public VideoCardInterfaceController(IVideoCardInterfaceService videoCardInterfaceService)
        {
            _videoCardInterfaceService = videoCardInterfaceService;
        }
        // GET: VideoCardInterface
        public ActionResult Index()
        {
            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().OrderBy(m => m.Name);

            var model = videoCardInterfaces.Select(m => new VideoCardInterfaceViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Multiplier = m.Multiplier,
                Version = m.Version
            });

            return View(model);
        }

        // GET: VideoCardInterface
        public ActionResult PartialIndex(string search = "")
        {
            var videoCardInterfaces = _videoCardInterfaceService.GetVideoCardInterfaces().OrderBy(m => m.Name);

            var model = videoCardInterfaces.Select(m => new VideoCardInterfaceViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Multiplier = m.Multiplier,
                Version = m.Version
            }).Where(m => string.IsNullOrEmpty(search) || m.FullName.Contains(search));

            return PartialView("Index", model);
        }

        // GET: VideoCardInterface/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VideoCardInterface/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: VideoCardInterface/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(VideoCardInterfaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var videoCardInterface = new VideoCardInterface()
                {
                    Name = model.Name,
                    Multiplier = model.Multiplier,
                    Version = model.Version
                };

                var result = _videoCardInterfaceService.CreateVideoCardInterface(videoCardInterface);

                model.Id = videoCardInterface.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // GET: VideoCardInterface/Create
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var videoCardInterface = _videoCardInterfaceService.GetVideoCardInterface(id);
            if (videoCardInterface == null)
            {
                return NotFound();
            }

            var model = new VideoCardInterfaceViewModel()
            {
                Name = videoCardInterface.Name,
                Version = videoCardInterface.Version,
                Multiplier = videoCardInterface.Multiplier
            };

            return PartialView("Create", model);
        }

        // POST: VideoCardInterface/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(VideoCardInterfaceViewModel model)
        { 
            var videoCardInterface = _videoCardInterfaceService.GetVideoCardInterface(model.Id);
            if (videoCardInterface == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                videoCardInterface.Name = model.Name;
                videoCardInterface.Multiplier = model.Multiplier;
                videoCardInterface.Version = model.Version;

                var result = _videoCardInterfaceService.UpdateVideoCardInterface(videoCardInterface);

                model.Id = videoCardInterface.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // POST: VideoCardInterface/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var videoCardInterface = _videoCardInterfaceService.GetVideoCardInterface(id);
                if(videoCardInterface == null)
                {
                    return NotFound();
                }

                var result = _videoCardInterfaceService.DeleteVideoCardInterface(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}