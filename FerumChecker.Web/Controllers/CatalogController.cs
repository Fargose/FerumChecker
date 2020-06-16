using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerumChecker.Web.Controllers
{
    [Authorize]
    public class CatalogController : Controller
    {
        public IActionResult Index(string startView = "")
        {
            ViewBag.StartView = startView;
            return View();
        }
    }
}