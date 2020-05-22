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

    public class OuterMemoryInterfaceController : Controller
    {

        private readonly IOuterMemoryInterfaceService _motherBoardInterfaceService;
        public OuterMemoryInterfaceController(IOuterMemoryInterfaceService motherBoardInterfaceService)
        {
            _motherBoardInterfaceService = motherBoardInterfaceService;
        }
        // GET: OuterMemoryInterface
        public ActionResult Index()
        {
            var motherBoardInterfaces = _motherBoardInterfaceService.GetOuterMemoryInterfaces().OrderBy(m => m.Name);

            var model = motherBoardInterfaces.Select(m => new OuterMemoryInterfaceViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: OuterMemoryInterface
        public ActionResult PartialIndex(string search = "")
        {
            var motherBoardInterfaces = _motherBoardInterfaceService.GetOuterMemoryInterfaces().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search)); ;

            var model = motherBoardInterfaces.Select(m => new OuterMemoryInterfaceViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: OuterMemoryInterface/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OuterMemoryInterface/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OuterMemoryInterface/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(OuterMemoryInterfaceViewModel model)
        {
            var motherBoardInterface = new OuterMemoryInterface()
            {
                Name = model.Name
            };

            var result = _motherBoardInterfaceService.CreateOuterMemoryInterface(motherBoardInterface);
            if (result.Succedeed)
            {
                return Json(motherBoardInterface);
            }

            return Json(result);
        }


        // POST: OuterMemoryInterface/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(OuterMemoryInterfaceViewModel model)
        {
            var motherBoardInterface = _motherBoardInterfaceService.GetOuterMemoryInterface(model.Id);
            if(motherBoardInterface == null)
            {
                return NotFound();
            }

            motherBoardInterface.Name = model.Name;
            var result = _motherBoardInterfaceService.UpdateOuterMemoryInterface(motherBoardInterface);

            if (result.Succedeed)
            {
                return Json(motherBoardInterface);
            }

            return Json(result);
        }


        // POST: OuterMemoryInterface/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var motherBoardInterface = _motherBoardInterfaceService.GetOuterMemoryInterface(id);
                if(motherBoardInterface == null)
                {
                    return NotFound();
                }

                var result = _motherBoardInterfaceService.DeleteOuterMemoryInterface(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}