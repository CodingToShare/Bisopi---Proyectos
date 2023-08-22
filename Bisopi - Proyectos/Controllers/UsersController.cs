using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Extensions;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ViewModels;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Bisopi___Proyectos.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DataContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }  
    }
}
