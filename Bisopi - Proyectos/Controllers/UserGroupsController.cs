using Bisopi___Proyectos.Attributes;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Extensions;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bisopi___Proyectos.Controllers
{
    [BisopiRoleAuth("UserGroups")]
    [BisopiGroupAuth("UserGroups")]
    public class UserGroupsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public UserGroupsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DataContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userGroupsViewModel = new List<UserGroupsViewModel>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new UserGroupsViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Groups = await GetUserGroups(user)
                };
                userGroupsViewModel.Add(thisViewModel);
            }
            return View(userGroupsViewModel);
        }

        private async Task<List<string>> GetUserGroups(ApplicationUser user)
        {
            return await _userManager.GetGroupsAsync(_context, user);
        }

        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserGroupsViewModel>();
            var groups = await _context.Groups.ToListAsync();
            foreach (var group in groups)
            {
                var userGroupViewModel = new ManageUserGroupsViewModel
                {
                    GroupId = group.Id,
                    GroupName = group.Name
                };

                if (await _userManager.IsInGroupAsync(_context, user, group.Name))
                {
                    userGroupViewModel.Selected = true;
                }
                else
                {
                    userGroupViewModel.Selected = false;
                }

                model.Add(userGroupViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserGroupsViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }

            var selectedCount = model.Where(x => x.Selected).Count();
            if (selectedCount > 1)
            {
                ModelState.AddModelError("", "Un usuario no puede pertenecer a varios grupos");
                ViewBag.UserName = user.UserName;
                return View(model);
            }

            var groups = await GetUserGroups(user);

            var result = await _userManager.OverrideGroups(_context, user, model.Where(x => x.Selected).Select(y => y.GroupName));
            if (!result)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                ViewBag.UserName = user.UserName;
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
