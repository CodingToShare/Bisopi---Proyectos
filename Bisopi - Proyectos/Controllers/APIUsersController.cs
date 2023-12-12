using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Extensions;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ViewModels;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bisopi___Proyectos.Controllers
{
    public class APIUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;
        private readonly ApplicationDbContext _apcontext;

        public APIUsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DataContext context, ApplicationDbContext apcontext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _apcontext = apcontext;
        }

        [HttpGet]
        public IActionResult GetUsersByRole(string role)
        {
            var usersWithRole = _userManager.GetUsersInRoleAsync(role).Result;
            var usersData = usersWithRole.Where(x => x.IsActive).Select(u => new { u.Id, u.UserName, u.FirstName, u.LastName }).ToList();

            var resultList = new List<UserData>();

            foreach (var user in usersData)
            {
                var result = new UserData();

                result.UserDataID = Guid.Parse(user.Id);
                result.FirstNameAndLastNAme = $"{user.FirstName} {user.LastName}";

                resultList.Add(result);
            }
            return Ok(resultList.OrderBy(x => x.FirstNameAndLastNAme));
        }

        [HttpGet]
        public async Task<IActionResult> UsersLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _apcontext.Users
                         orderby i.FirstName
                         select new
                         {
                             Value = i.Id,
                             Text = i.FirstName + " " + i.LastName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> RolesLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _apcontext.Roles
                         orderby i.Name
                         select new
                         {
                             Value = i.Id,
                             Text = i.Name
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.ToList();
            var usersData = users.Where(x => x.IsActive).Select(u => new { u.Id, u.UserName, u.FirstName, u.LastName }).ToList();

            var resultList = new List<UserData>();

            foreach (var user in usersData)
            {
                var result = new UserData();

                result.UserDataID = Guid.Parse(user.Id);
                result.FirstNameAndLastNAme = $"{user.FirstName} {user.LastName}";

                resultList.Add(result);
            }
            return Ok(resultList.OrderBy(x => x.FirstNameAndLastNAme));
        }

        [HttpGet]
        public IActionResult GetUsersResourcePlaning(Guid id)
        {
            var resourcePlanings = _context.ResourcesPlannings.Where(x => x.ProjectID == id).Select(i => i.ResourceID);

            var usersData = new List<ApplicationUser>();


            foreach (var resourcePlaning in resourcePlanings)
            {
                var users = _apcontext.Users.Where(x => x.Id == resourcePlaning.ToString()).FirstOrDefault();

                usersData.Add(users);

            }

            var resultList = new List<UserData>();

            foreach (var user in usersData)
            {
                var result = new UserData();

                result.UserDataID = Guid.Parse(user.Id);
                result.FirstNameAndLastNAme = $"{user.FirstName} {user.LastName}";

                resultList.Add(result);
            }
            return Ok(resultList);
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesData = roles.Select(u => new { u.Id, u.Name }).Where(x => x.Name != "SuperAdmin" && x.Name != "Admin").ToList();

            var resultList = new List<RolesData>();

            foreach (var role in rolesData)
            {
                var result = new RolesData();

                result.RolesDataID = Guid.Parse(role.Id);
                result.Name = $"{role.Name}";

                resultList.Add(result);
            }

            return Ok(resultList);
        }

        
    }

    public class UserData
    {
        public Guid UserDataID { get; set; }
        public string FirstNameAndLastNAme { get; set; }
    }

    public class RolesData
    {
        public Guid RolesDataID { get; set; }
        public string Name { get; set; }
    }
}
