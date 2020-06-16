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
    public class OuterMemoryFormFactorController : Controller
    {

        private readonly IOuterMemoryFormFactorService _motherBoardFormFactorService;
        public OuterMemoryFormFactorController(IOuterMemoryFormFactorService motherBoardFormFactorService)
        {
            _motherBoardFormFactorService = motherBoardFormFactorService;
        }
        // GET: OuterMemoryFormFactor
        public ActionResult Index()
        {
            var motherBoardFormFactors = _motherBoardFormFactorService.GetOuterMemoryFormFactors().OrderBy(m => m.Name);

            var model = motherBoardFormFactors.Select(m => new OuterMemoryFormFactorViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: OuterMemoryFormFactor
        public ActionResult PartialIndex(string search = "")
        {
            var motherBoardFormFactors = _motherBoardFormFactorService.GetOuterMemoryFormFactors().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search)); ;

            var model = motherBoardFormFactors.Select(m => new OuterMemoryFormFactorViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: OuterMemoryFormFactor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OuterMemoryFormFactor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OuterMemoryFormFactor/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(OuterMemoryFormFactorViewModel model)
        {
            var motherBoardFormFactor = new OuterMemoryFormFactor()
            {
                Name = model.Name
            };

            var result = _motherBoardFormFactorService.CreateOuterMemoryFormFactor(motherBoardFormFactor);
            if (result.Succedeed)
            {
                return Json(motherBoardFormFactor);
            }

            return Json(result);
        }


        // POST: OuterMemoryFormFactor/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(OuterMemoryFormFactorViewModel model)
        {
            var motherBoardFormFactor = _motherBoardFormFactorService.GetOuterMemoryFormFactor(model.Id);
            if(motherBoardFormFactor == null)
            {
                return NotFound();
            }

            motherBoardFormFactor.Name = model.Name;
            var result = _motherBoardFormFactorService.UpdateOuterMemoryFormFactor(motherBoardFormFactor);

            if (result.Succedeed)
            {
                return Json(motherBoardFormFactor);
            }

            return Json(result);
        }


        // POST: OuterMemoryFormFactor/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var motherBoardFormFactor = _motherBoardFormFactorService.GetOuterMemoryFormFactor(id);
                if(motherBoardFormFactor == null)
                {
                    return NotFound();
                }

                var result = _motherBoardFormFactorService.DeleteOuterMemoryFormFactor(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}