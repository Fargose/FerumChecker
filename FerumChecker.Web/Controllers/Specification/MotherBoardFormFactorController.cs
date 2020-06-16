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
    public class MotherBoardFormFactorController : Controller
    {

        private readonly IMotherBoardFormFactorService _motherBoardFormFactorService;
        public MotherBoardFormFactorController(IMotherBoardFormFactorService motherBoardFormFactorService)
        {
            _motherBoardFormFactorService = motherBoardFormFactorService;
        }
        // GET: MotherBoardFormFactor
        public ActionResult Index()
        {
            var motherBoardFormFactors = _motherBoardFormFactorService.GetMotherBoardFormFactors().OrderBy(m => m.Name);

            var model = motherBoardFormFactors.Select(m => new MotherBoardFormFactorViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: MotherBoardFormFactor
        public ActionResult PartialIndex(string search = "")
        {
            var motherBoardFormFactors = _motherBoardFormFactorService.GetMotherBoardFormFactors().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search)); ;

            var model = motherBoardFormFactors.Select(m => new MotherBoardFormFactorViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: MotherBoardFormFactor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MotherBoardFormFactor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MotherBoardFormFactor/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(MotherBoardFormFactorViewModel model)
        {
            var motherBoardFormFactor = new MotherBoardFormFactor()
            {
                Name = model.Name
            };

            var result = _motherBoardFormFactorService.CreateMotherBoardFormFactor(motherBoardFormFactor);
            if (result.Succedeed)
            {
                return Json(motherBoardFormFactor);
            }

            return Json(result);
        }


        // POST: MotherBoardFormFactor/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(MotherBoardFormFactorViewModel model)
        {
            var motherBoardFormFactor = _motherBoardFormFactorService.GetMotherBoardFormFactor(model.Id);
            if(motherBoardFormFactor == null)
            {
                return NotFound();
            }

            motherBoardFormFactor.Name = model.Name;
            var result = _motherBoardFormFactorService.UpdateMotherBoardFormFactor(motherBoardFormFactor);

            if (result.Succedeed)
            {
                return Json(motherBoardFormFactor);
            }

            return Json(result);
        }


        // POST: MotherBoardFormFactor/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var motherBoardFormFactor = _motherBoardFormFactorService.GetMotherBoardFormFactor(id);
                if(motherBoardFormFactor == null)
                {
                    return NotFound();
                }

                var result = _motherBoardFormFactorService.DeleteMotherBoardFormFactor(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}