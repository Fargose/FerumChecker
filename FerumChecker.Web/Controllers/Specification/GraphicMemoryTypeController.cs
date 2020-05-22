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

    public class GraphicMemoryTypeController : Controller
    {

        private readonly IGraphicMemoryTypeService _graphicMemoryTypeService;
        public GraphicMemoryTypeController(IGraphicMemoryTypeService graphicMemoryTypeService)
        {
            _graphicMemoryTypeService = graphicMemoryTypeService;
        }
        // GET: GraphicMemoryType
        public ActionResult Index()
        {
            var graphicMemoryTypes = _graphicMemoryTypeService.GetGraphicMemoryTypes().OrderBy(m => m.Name);

            var model = graphicMemoryTypes.Select(m => new GraphicMemoryTypeViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return View(model);
        }

        // GET: GraphicMemoryType
        public ActionResult PartialIndex(string search = "")
        {
            var graphicMemoryTypes = _graphicMemoryTypeService.GetGraphicMemoryTypes().OrderBy(m => m.Name).Where(m => string.IsNullOrEmpty(search) || m.Name.Contains(search)); ;

            var model = graphicMemoryTypes.Select(m => new GraphicMemoryTypeViewModel
            {
                Id = m.Id,
                Name = m.Name
            });

            return PartialView("Index", model);
        }

        // GET: GraphicMemoryType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GraphicMemoryType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GraphicMemoryType/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(GraphicMemoryTypeViewModel model)
        {
            var graphicMemoryType = new GraphicMemoryType()
            {
                Name = model.Name
            };

            var result = _graphicMemoryTypeService.CreateGraphicMemoryType(graphicMemoryType);
            if (result.Succedeed)
            {
                return Json(graphicMemoryType);
            }

            return Json(result);
        }


        // POST: GraphicMemoryType/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(GraphicMemoryTypeViewModel model)
        {
            var graphicMemoryType = _graphicMemoryTypeService.GetGraphicMemoryType(model.Id);
            if(graphicMemoryType == null)
            {
                return NotFound();
            }

            graphicMemoryType.Name = model.Name;
            var result = _graphicMemoryTypeService.UpdateGraphicMemoryType(graphicMemoryType);

            if (result.Succedeed)
            {
                return Json(graphicMemoryType);
            }

            return Json(result);
        }


        // POST: GraphicMemoryType/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var graphicMemoryType = _graphicMemoryTypeService.GetGraphicMemoryType(id);
                if(graphicMemoryType == null)
                {
                    return NotFound();
                }

                var result = _graphicMemoryTypeService.DeleteGraphicMemoryType(id);
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}