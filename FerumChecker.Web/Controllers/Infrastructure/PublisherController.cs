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

    public class PublisherController : Controller
    {

        private readonly IPublisherService _publisherService;
        private readonly ICountryService _countryService;
        public PublisherController(IPublisherService publisherService, ICountryService countryService)
        {
            _publisherService = publisherService;
            _countryService = countryService;
        }
        // GET: Publisher
        public ActionResult Index()
        {
            var publishers = _publisherService.GetPublishers().OrderBy(m => m.Name);

            var model = publishers.Select(m => new PublisherViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description
            });

            return View(model);
        }

        // GET: Publisher
        public ActionResult PartialIndex(string search = "")
        {
            var publishers = _publisherService.GetPublishers().OrderBy(m => m.Name);

            var model = publishers.Select(m => new PublisherViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description
            }).Where(m => string.IsNullOrEmpty(search) || m.FullName.Contains(search));

            return PartialView("Index", model);
        }

        // GET: Publisher/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Publisher/Create
        public ActionResult Create()
        {
            var books = _countryService.GetCountrys();
            ViewBag.Countries = new SelectList(books, "Id", "Name");
            return PartialView("Create");
        }

        // POST: Publisher/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(PublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var publisher = new Publisher()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CountryId = model.CountryId
                };

                var result = _publisherService.CreatePublisher(publisher);

                model.Id = publisher.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // GET: Publisher/Create
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var publisher = _publisherService.GetPublisher(id);
            if (publisher == null)
            {
                return NotFound();
            }

            var model = new PublisherViewModel()
            {
                Name = publisher.Name,
                Description = publisher.Description,
                CountryId = publisher.CountryId
            };


            var books = _countryService.GetCountrys();
            ViewBag.Countries = new SelectList(books, "Id", "Name");
            return PartialView("Create",  model);
        }

        // POST: Publisher/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(PublisherViewModel model)
        { 
            var publisher = _publisherService.GetPublisher(model.Id);
            if (publisher == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                publisher.Name = model.Name;
                publisher.Description = model.Description;
                publisher.CountryId = model.CountryId;


                var result = _publisherService.UpdatePublisher(publisher);

                model.Id = publisher.Id;

                if (result.Succedeed)
                {
                    return Json(model);
                }

                return NotFound(result);
            }
            return PartialView(model);
        }


        // POST: Publisher/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var publisher = _publisherService.GetPublisher(id);
                if(publisher == null)
                {
                    return NotFound();
                }

                var result = _publisherService.DeletePublisher(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}