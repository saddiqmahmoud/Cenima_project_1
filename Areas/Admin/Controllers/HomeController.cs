﻿using Microsoft.AspNetCore.Mvc;

namespace Cenima_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
