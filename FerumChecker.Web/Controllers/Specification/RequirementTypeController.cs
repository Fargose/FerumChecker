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
    public class RequirementTypeController : Controller
    {

        private readonly IRequirementTypeService _requirementTypeService;
        public RequirementTypeController(IRequirementTypeService requirementTypeService)
        {
            _requirementTypeService = requirementTypeService;
        }
        // GET: RequirementType
        public ActionResult Index()
        {
            var requirementTypes = _requirementTypeService.GetRequirementTypes().OrderBy(m => m.Name);

            var model = requirementTypes.Select(m => new RequirementTypeViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: RequirementType
        public ActionResult PartialIndex(string search = "")
        {
            var requirementTypes = _requirementTypeService.GetRequirementTypes().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search)); ;

            var model = requirementTypes.Select(m => new RequirementTypeViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: RequirementType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RequirementType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequirementType/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(RequirementTypeViewModel model)
        {
            var requirementType = new RequirementType()
            {
                Name = model.Name
            };

            var result = _requirementTypeService.CreateRequirementType(requirementType);
            if (result.Succedeed)
            {
                return Json(requirementType);
            }

            return Json(result);
        }


        // POST: RequirementType/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(RequirementTypeViewModel model)
        {
            var requirementType = _requirementTypeService.GetRequirementType(model.Id);
            if(requirementType == null)
            {
                return NotFound();
            }

            requirementType.Name = model.Name;
            var result = _requirementTypeService.UpdateRequirementType(requirementType);

            if (result.Succedeed)
            {
                return Json(requirementType);
            }

            return Json(result);
        }


        // POST: RequirementType/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var requirementType = _requirementTypeService.GetRequirementType(id);
                if(requirementType == null)
                {
                    return NotFound();
                }

                var result = _requirementTypeService.DeleteRequirementType(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}