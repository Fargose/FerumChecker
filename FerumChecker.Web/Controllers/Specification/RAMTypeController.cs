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
    public class RAMTypeController : Controller
    {

        private readonly IRAMTypeService _ramTypeService;
        public RAMTypeController(IRAMTypeService ramTypeService)
        {
            _ramTypeService = ramTypeService;
        }
        // GET: RAMType
        public ActionResult Index()
        {
            var ramTypes = _ramTypeService.GetRAMTypes().OrderBy(m => m.Name);

            var model = ramTypes.Select(m => new RAMTypeViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: RAMType
        public ActionResult PartialIndex(string search = "")
        {
            var ramTypes = _ramTypeService.GetRAMTypes().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search));

            var model = ramTypes.Select(m => new RAMTypeViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: RAMType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RAMType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RAMType/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(RAMTypeViewModel model)
        {
            var ramType = new RAMType()
            {
                Name = model.Name
            };

            var result = _ramTypeService.CreateRAMType(ramType);
            if (result.Succedeed)
            {
                return Json(ramType);
            }

            return Json(result);
        }


        // POST: RAMType/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(RAMTypeViewModel model)
        {
            var ramType = _ramTypeService.GetRAMType(model.Id);
            if(ramType == null)
            {
                return NotFound();
            }

            ramType.Name = model.Name;
            var result = _ramTypeService.UpdateRAMType(ramType);

            if (result.Succedeed)
            {
                return Json(ramType);
            }

            return Json(result);
        }


        // POST: RAMType/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var ramType = _ramTypeService.GetRAMType(id);
                if(ramType == null)
                {
                    return NotFound();
                }

                var result = _ramTypeService.DeleteRAMType(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}