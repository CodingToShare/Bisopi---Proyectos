using Bisopi___Proyectos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bisopi___Proyectos.Controllers
{
    public class APIUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public APIUsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetUsersByRole(string role)
        {
            var usersWithRole = _userManager.GetUsersInRoleAsync(role).Result;
            var usersData = usersWithRole.Select(u => new { u.Id, u.UserName, u.FirstName, u.LastName }).ToList();

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
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.ToList();
            var usersData = users.Select(u => new { u.Id, u.UserName, u.FirstName, u.LastName }).ToList();

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
            var rolesData = roles.Select(u => new { u.Id, u.Name }).ToList();

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
