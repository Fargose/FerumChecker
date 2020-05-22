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

    public class MotherBoardNorthBridgeController : Controller
    {

        private readonly IMotherBoardNorthBridgeService _motherBoardNorthBridgeService;
        public MotherBoardNorthBridgeController(IMotherBoardNorthBridgeService motherBoardNorthBridgeService)
        {
            _motherBoardNorthBridgeService = motherBoardNorthBridgeService;
        }
        // GET: MotherBoardNorthBridge
        public ActionResult Index()
        {
            var motherBoardNorthBridges = _motherBoardNorthBridgeService.GetMotherBoardNorthBridges().OrderBy(m => m.Name);

            var model = motherBoardNorthBridges.Select(m => new MotherBoardNorthBridgeViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: MotherBoardNorthBridge
        public ActionResult PartialIndex(string search = "")
        {
            var motherBoardNorthBridges = _motherBoardNorthBridgeService.GetMotherBoardNorthBridges().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search)); ;

            var model = motherBoardNorthBridges.Select(m => new MotherBoardNorthBridgeViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: MotherBoardNorthBridge/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MotherBoardNorthBridge/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MotherBoardNorthBridge/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(MotherBoardNorthBridgeViewModel model)
        {
            var motherBoardNorthBridge = new MotherBoardNorthBridge()
            {
                Name = model.Name
            };

            var result = _motherBoardNorthBridgeService.CreateMotherBoardNorthBridge(motherBoardNorthBridge);
            if (result.Succedeed)
            {
                return Json(motherBoardNorthBridge);
            }

            return Json(result);
        }


        // POST: MotherBoardNorthBridge/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(MotherBoardNorthBridgeViewModel model)
        {
            var motherBoardNorthBridge = _motherBoardNorthBridgeService.GetMotherBoardNorthBridge(model.Id);
            if(motherBoardNorthBridge == null)
            {
                return NotFound();
            }

            motherBoardNorthBridge.Name = model.Name;
            var result = _motherBoardNorthBridgeService.UpdateMotherBoardNorthBridge(motherBoardNorthBridge);

            if (result.Succedeed)
            {
                return Json(motherBoardNorthBridge);
            }

            return Json(result);
        }


        // POST: MotherBoardNorthBridge/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var motherBoardNorthBridge = _motherBoardNorthBridgeService.GetMotherBoardNorthBridge(id);
                if(motherBoardNorthBridge == null)
                {
                    return NotFound();
                }

                var result = _motherBoardNorthBridgeService.DeleteMotherBoardNorthBridge(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}