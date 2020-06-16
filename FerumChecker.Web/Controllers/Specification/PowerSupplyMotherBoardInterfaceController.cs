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
    public class PowerSupplyMotherBoardInterfaceController : Controller
    {

        private readonly IPowerSupplyMotherBoardInterfaceService _powerSupplyMotherBoardInterfaceService;
        public PowerSupplyMotherBoardInterfaceController(IPowerSupplyMotherBoardInterfaceService powerSupplyMotherBoardInterfaceService)
        {
            _powerSupplyMotherBoardInterfaceService = powerSupplyMotherBoardInterfaceService;
        }
        // GET: PowerSupplyMotherBoardInterface
        public ActionResult Index()
        {
            var powerSupplyMotherBoardInterfaces = _powerSupplyMotherBoardInterfaceService.GetPowerSupplyMotherBoardInterfaces().OrderBy(m => m.Name);

            var model = powerSupplyMotherBoardInterfaces.Select(m => new PowerSupplyMotherBoardInterfaceViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: PowerSupplyMotherBoardInterface
        public ActionResult PartialIndex(string search = "")
        {
            var powerSupplyMotherBoardInterfaces = _powerSupplyMotherBoardInterfaceService.GetPowerSupplyMotherBoardInterfaces().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search)); ;

            var model = powerSupplyMotherBoardInterfaces.Select(m => new PowerSupplyMotherBoardInterfaceViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: PowerSupplyMotherBoardInterface/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PowerSupplyMotherBoardInterface/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PowerSupplyMotherBoardInterface/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(PowerSupplyMotherBoardInterfaceViewModel model)
        {
            var powerSupplyMotherBoardInterface = new PowerSupplyMotherBoardInterface()
            {
                Name = model.Name
            };

            var result = _powerSupplyMotherBoardInterfaceService.CreatePowerSupplyMotherBoardInterface(powerSupplyMotherBoardInterface);
            if (result.Succedeed)
            {
                return Json(powerSupplyMotherBoardInterface);
            }

            return Json(result);
        }


        // POST: PowerSupplyMotherBoardInterface/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(PowerSupplyMotherBoardInterfaceViewModel model)
        {
            var powerSupplyMotherBoardInterface = _powerSupplyMotherBoardInterfaceService.GetPowerSupplyMotherBoardInterface(model.Id);
            if(powerSupplyMotherBoardInterface == null)
            {
                return NotFound();
            }

            powerSupplyMotherBoardInterface.Name = model.Name;
            var result = _powerSupplyMotherBoardInterfaceService.UpdatePowerSupplyMotherBoardInterface(powerSupplyMotherBoardInterface);

            if (result.Succedeed)
            {
                return Json(powerSupplyMotherBoardInterface);
            }

            return Json(result);
        }


        // POST: PowerSupplyMotherBoardInterface/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var powerSupplyMotherBoardInterface = _powerSupplyMotherBoardInterfaceService.GetPowerSupplyMotherBoardInterface(id);
                if(powerSupplyMotherBoardInterface == null)
                {
                    return NotFound();
                }

                var result = _powerSupplyMotherBoardInterfaceService.DeletePowerSupplyMotherBoardInterface(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}