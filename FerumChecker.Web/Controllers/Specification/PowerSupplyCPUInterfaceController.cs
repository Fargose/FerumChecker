using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FerumChecker.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PowerSupplyCPUInterfaceController : Controller
    {

        private readonly IPowerSupplyCPUInterfaceService _powerSupplyCPUInterfaceService;
        public PowerSupplyCPUInterfaceController(IPowerSupplyCPUInterfaceService powerSupplyCPUInterfaceService)
        {
            _powerSupplyCPUInterfaceService = powerSupplyCPUInterfaceService;
        }
        // GET: PowerSupplyCPUInterface
        public ActionResult Index()
        {
            var powerSupplyCPUInterfaces = _powerSupplyCPUInterfaceService.GetPowerSupplyCPUInterfaces().OrderBy(m => m.Name);

            var model = powerSupplyCPUInterfaces.Select(m => new PowerSupplyCPUInterfaceViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: PowerSupplyCPUInterface
        public ActionResult PartialIndex(string search = "")
        {
            var powerSupplyCPUInterfaces = _powerSupplyCPUInterfaceService.GetPowerSupplyCPUInterfaces().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search)); ;

            var model = powerSupplyCPUInterfaces.Select(m => new PowerSupplyCPUInterfaceViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: PowerSupplyCPUInterface/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PowerSupplyCPUInterface/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PowerSupplyCPUInterface/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(PowerSupplyCPUInterfaceViewModel model)
        {
            var powerSupplyCPUInterface = new PowerSupplyCPUInterface()
            {
                Name = model.Name
            };

            var result = _powerSupplyCPUInterfaceService.CreatePowerSupplyCPUInterface(powerSupplyCPUInterface);
            if (result.Succedeed)
            {
                return Json(powerSupplyCPUInterface);
            }

            return Json(result);
        }


        // POST: PowerSupplyCPUInterface/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(PowerSupplyCPUInterfaceViewModel model)
        {
            var powerSupplyCPUInterface = _powerSupplyCPUInterfaceService.GetPowerSupplyCPUInterface(model.Id);
            if(powerSupplyCPUInterface == null)
            {
                return NotFound();
            }

            powerSupplyCPUInterface.Name = model.Name;
            var result = _powerSupplyCPUInterfaceService.UpdatePowerSupplyCPUInterface(powerSupplyCPUInterface);

            if (result.Succedeed)
            {
                return Json(powerSupplyCPUInterface);
            }

            return Json(result);
        }


        // POST: PowerSupplyCPUInterface/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var powerSupplyCPUInterface = _powerSupplyCPUInterfaceService.GetPowerSupplyCPUInterface(id);
                if(powerSupplyCPUInterface == null)
                {
                    return NotFound();
                }

                var result = _powerSupplyCPUInterfaceService.DeletePowerSupplyCPUInterface(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}