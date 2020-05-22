using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FerumChecker.Web.Controllers
{

    public class CountryController : Controller
    {

        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        // GET: Country
        public ActionResult Index()
        {
            var countrys = _countryService.GetCountrys().OrderBy(m => m.Name);

            var model = countrys.Select(m => new CountryViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: Country
        public ActionResult PartialIndex(string search = "")
        {
            var countrys = _countryService.GetCountrys().OrderBy(m => m.Name);

            var model = countrys.Select(m => new CountryViewModel
            {
                Id = m.Id,
                Name = m.Name
            }).Where(m => string.IsNullOrEmpty(search) || m.FullName.Contains(search));

            return PartialView("Index", model);
        }

        // GET: Country/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Country/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: Country/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var country = new Country()
                {
                    Name = model.Name
                };

                var result = _countryService.CreateCountry(country);

                model.Id = country.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // GET: Country/Create
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var country = _countryService.GetCountry(id);
            if (country == null)
            {
                return NotFound();
            }

            var model = new CountryViewModel()
            {
                Name = country.Name
            };

            return PartialView("Create", model);
        }

        // POST: Country/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CountryViewModel model)
        { 
            var country = _countryService.GetCountry(model.Id);
            if (country == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                country.Name = model.Name;


                var result = _countryService.UpdateCountry(country);

                model.Id = country.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // POST: Country/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var country = _countryService.GetCountry(id);
                if(country == null)
                {
                    return NotFound();
                }

                var result = _countryService.DeleteCountry(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}