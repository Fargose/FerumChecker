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

    public class CPUSocketController : Controller
    {

        private readonly ICPUSocketService _cpuSocketService;
        public CPUSocketController(ICPUSocketService cpuSocketService)
        {
            _cpuSocketService = cpuSocketService;
        }
        // GET: CPUSocket
        public ActionResult Index()
        {
            var cpuSockets = _cpuSocketService.GetCPUSockets().OrderBy(m => m.Name);

            var model = cpuSockets.Select(m => new CPUSocketViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: CPUSocket
        public ActionResult PartialIndex(string search = "")
        {
            var cpuSockets = _cpuSocketService.GetCPUSockets().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search));

            var model = cpuSockets.Select(m => new CPUSocketViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: CPUSocket/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CPUSocket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CPUSocket/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(CPUSocketViewModel model)
        {
            var cpuSocket = new CPUSocket()
            {
                Name = model.Name
            };

            var result = _cpuSocketService.CreateCPUSocket(cpuSocket);
            if (result.Succedeed)
            {
                return Json(cpuSocket);
            }

            return Json(result);
        }


        // POST: CPUSocket/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CPUSocketViewModel model)
        {
            var cpuSocket = _cpuSocketService.GetCPUSocket(model.Id);
            if(cpuSocket == null)
            {
                return NotFound();
            }

            cpuSocket.Name = model.Name;
            var result = _cpuSocketService.UpdateCPUSocket(cpuSocket);

            if (result.Succedeed)
            {
                return Json(cpuSocket);
            }

            return Json(result);
        }


        // POST: CPUSocket/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var cpuSocket = _cpuSocketService.GetCPUSocket(id);
                if(cpuSocket == null)
                {
                    return NotFound();
                }

                var result = _cpuSocketService.DeleteCPUSocket(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}