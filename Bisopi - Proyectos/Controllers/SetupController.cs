﻿using Bisopi___Proyectos.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bisopi___Proyectos.Controllers
{
    [Authorize]
    public class SetupController : Controller
    {
        [BisopiRoleAuth("Configuracion")]
        //[BisopiGroupAuth("Configuracion")]
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

        public IActionResult QuoteStatus()
        {
            return View();
        }

        public IActionResult ProposalStatus()
        {
            return View();
        }
        public IActionResult ProjectTaskStatus()
        {
            return View();
        }
        public IActionResult ProjectTaskGroup()
        {
            return View();
        }

        public IActionResult City()
        {
            return View();
        }

        public IActionResult Group()
        {
            return View();
        }

        public IActionResult RepresentativeMarketRate()
        {
            return View();
        }

        public IActionResult RetentionPercentage()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Study()
        {
            return View();
        }
        public IActionResult Tool()
        {
            return View();
        }

        public IActionResult Level()
        {
            return View();
        }

        public IActionResult ActivityType()
        {
            return View();
        }

        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult Seniority()
        {
            return View();
        }

    }
}
