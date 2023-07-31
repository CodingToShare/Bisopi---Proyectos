using Bisopi___Proyectos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bisopi___Proyectos.Controllers
{
    public class APIUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public APIUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }

    public class UserData
    {
        public Guid UserDataID { get; set; }
        public string FirstNameAndLastNAme { get; set; }
    }
}
