using Bisopi___Proyectos.Alerts;
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

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserViewModel model)
        {
            return RedirectToAction("Users","Setup").WithSuccess("El recurso se registro satisfactoriamente");
        }

        public async Task<IActionResult> GetUsers(DataSourceLoadOptions loadOptions)
        {
            var users = await _userManager.Users.ToListAsync();
            var userGroupsViewModel = new List<UserViewModel>();

            foreach (ApplicationUser user in users)
            {
                var city = _context.Cities.Where(x => x.Id == user.CityId).FirstOrDefault();

                if (city != null)
                {
                    var thisViewModel = new UserViewModel
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Phone = user.PhoneNumber,
                        City = $"{city.Name} - {city.Abbreviation}",
                        IsActive = user.IsActive,
                        Groups = await GetUserGroups(user),
                        Roles = await GetUserRoles(user)
                    };
                    userGroupsViewModel.Add(thisViewModel);
                }
                else
                {
                    var thisViewModel = new UserViewModel
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Phone = user.PhoneNumber,
                        IsActive = user.IsActive,
                        Groups = await GetUserGroups(user),
                        Roles = await GetUserRoles(user)
                    };
                    userGroupsViewModel.Add(thisViewModel);
                }

            }
            return Json(DataSourceLoader.Load(userGroupsViewModel, loadOptions));
        }

        private async Task<List<string>> GetUserGroups(ApplicationUser user)
        {
            return await _userManager.GetGroupsAsync(_context, user);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}
