using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Web.ViewModel.Infrastructure;
using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerumChecker.Web.Controllers
{

    public class ManufacturerController : Controller
    {

        private readonly IManufacturerService _manufacturerService;
        private readonly ICountryService _countryService;
        public ManufacturerController(IManufacturerService manufacturerService, ICountryService countryService)
        {
            _manufacturerService = manufacturerService;
            _countryService = countryService;
        }
        // GET: Manufacturer
        public ActionResult Index()
        {
            var manufacturers = _manufacturerService.GetManufacturers().OrderBy(m => m.Name);

            var model = manufacturers.Select(m => new ManufacturerViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description
            });

            return View(model);
        }

        // GET: Manufacturer
        public ActionResult PartialIndex(string search = "")
        {
            var manufacturers = _manufacturerService.GetManufacturers().OrderBy(m => m.Name);

            var model = manufacturers.Select(m => new ManufacturerViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description
            }).Where(m => string.IsNullOrEmpty(search) || m.FullName.Contains(search));

            return PartialView("Index", model);
        }

        // GET: Manufacturer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manufacturer/Create
        public ActionResult Create()
        {
            var books = _countryService.GetCountrys();
            ViewBag.Countries = new SelectList(books, "Id", "Name");
            return PartialView("Create");
        }

        // POST: Manufacturer/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(ManufacturerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var manufacturer = new Manufacturer()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CountryId = model.CountryId
                };

                var result = _manufacturerService.CreateManufacturer(manufacturer);

                model.Id = manufacturer.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // GET: Manufacturer/Create
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var manufacturer = _manufacturerService.GetManufacturer(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            var model = new ManufacturerViewModel()
            {
                Name = manufacturer.Name,
                Description = manufacturer.Description,
                CountryId = manufacturer.CountryId
            };


            var books = _countryService.GetCountrys();
            ViewBag.Countries = new SelectList(books, "Id", "Name");
            return PartialView("Create",  model);
        }

        // POST: Manufacturer/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ManufacturerViewModel model)
        { 
            var manufacturer = _manufacturerService.GetManufacturer(model.Id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                manufacturer.Name = model.Name;
                manufacturer.Description = model.Description;
                manufacturer.CountryId = model.CountryId;


                var result = _manufacturerService.UpdateManufacturer(manufacturer);

                model.Id = manufacturer.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // POST: Manufacturer/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var manufacturer = _manufacturerService.GetManufacturer(id);
                if(manufacturer == null)
                {
                    return NotFound();
                }

                var result = _manufacturerService.DeleteManufacturer(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}