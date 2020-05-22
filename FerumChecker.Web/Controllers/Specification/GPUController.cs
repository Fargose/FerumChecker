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

    public class GPUController : Controller
    {

        private readonly IGPUService _gpuService;
        public GPUController(IGPUService gpuService)
        {
            _gpuService = gpuService;
        }
        // GET: GPU
        public ActionResult Index()
        {
            var gpus = _gpuService.GetGPUs().OrderBy(m => m.Name);

            var model = gpus.Select(m => new GPUViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: GPU
        public ActionResult PartialIndex(string search = "")
        {
            var gpus = _gpuService.GetGPUs().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search)); ;

            var model = gpus.Select(m => new GPUViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: GPU/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GPU/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GPU/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(GPUViewModel model)
        {
            var gpu = new GPU()
            {
                Name = model.Name
            };

            var result = _gpuService.CreateGPU(gpu);
            if (result.Succedeed)
            {
                return Json(gpu);
            }

            return Json(result);
        }


        // POST: GPU/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(GPUViewModel model)
        {
            var gpu = _gpuService.GetGPU(model.Id);
            if(gpu == null)
            {
                return NotFound();
            }

            gpu.Name = model.Name;
            var result = _gpuService.UpdateGPU(gpu);

            if (result.Succedeed)
            {
                return Json(gpu);
            }

            return Json(result);
        }


        // POST: GPU/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var gpu = _gpuService.GetGPU(id);
                if(gpu == null)
                {
                    return NotFound();
                }

                var result = _gpuService.DeleteGPU(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}