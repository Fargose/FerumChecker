using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Web.ViewModel.Infrastructure;
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DeveloperController : Controller
    {

        private readonly IDeveloperService _developerService;
        private readonly ICountryService _countryService;
        public DeveloperController(IDeveloperService developerService, ICountryService countryService)
        {
            _developerService = developerService;
            _countryService = countryService;
        }
        // GET: Developer
        public ActionResult Index()
        {
            var developers = _developerService.GetDevelopers().OrderBy(m => m.Name);

            var model = developers.Select(m => new DeveloperViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description
            });

            return View(model);
        }

        // GET: Developer
        public ActionResult PartialIndex(string search = "")
        {
            var developers = _developerService.GetDevelopers().OrderBy(m => m.Name);

            var model = developers.Select(m => new DeveloperViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description
            }).Where(m => string.IsNullOrEmpty(search) || m.FullName.Contains(search));

            return PartialView("Index", model);
        }

        // GET: Developer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Developer/Create
        public ActionResult Create()
        {
            var books = _countryService.GetCountrys();
            ViewBag.Countries = new SelectList(books, "Id", "Name");
            return PartialView("Create");
        }

        // POST: Developer/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(DeveloperViewModel model)
        {
            if (ModelState.IsValid)
            {
                var developer = new Developer()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CountryId = model.CountryId
                };

                var result = _developerService.CreateDeveloper(developer);

                model.Id = developer.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // GET: Developer/Create
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var developer = _developerService.GetDeveloper(id);
            if (developer == null)
            {
                return NotFound();
            }

            var model = new DeveloperViewModel()
            {
                Name = developer.Name,
                Description = developer.Description,
                CountryId = developer.CountryId
            };


            var books = _countryService.GetCountrys();
            ViewBag.Countries = new SelectList(books, "Id", "Name");
            return PartialView("Create",  model);
        }

        // POST: Developer/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(DeveloperViewModel model)
        { 
            var developer = _developerService.GetDeveloper(model.Id);
            if (developer == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                developer.Name = model.Name;
                developer.Description = model.Description;
                developer.CountryId = model.CountryId;


                var result = _developerService.UpdateDeveloper(developer);

                model.Id = developer.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // POST: Developer/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var developer = _developerService.GetDeveloper(id);
                if(developer == null)
                {
                    return NotFound();
                }

                var result = _developerService.DeleteDeveloper(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}