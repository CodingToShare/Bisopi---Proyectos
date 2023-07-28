﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bisopi___Proyectos.Controllers
{
    [Authorize]
    public class SetupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Countries()
        {
            return View();
        }

        public IActionResult Clients()
        {
            return View();
        }

        public IActionResult ProjectsStatus()
        {
            return View();
        }

        public IActionResult ProjectsTypes()
        {
            return View();
        }

        public IActionResult SupportsStatus()
        {
            return View();
        }

        public IActionResult Currencies()
        {
            return View();
        }
    }
}