using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerumChecker.Web.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class SpecificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}