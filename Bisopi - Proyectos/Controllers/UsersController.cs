using Bisopi___Proyectos.Alerts;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Extensions;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ViewModels;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Xml.Linq;

namespace Bisopi___Proyectos.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly DataContext _context;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserStore<ApplicationUser> userStore, DataContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _context = context;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(UserViewModel model)
        {
            MailAddress address = new MailAddress(model.Email);
            string userName = address.User;
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = model.IsActive,
                CityId = Guid.Parse(model.City),
                PhoneNumber = model.Phone

            };

            await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, "Bision2023.");

            var ListGroups = new List<ManageUserGroupsViewModel>();

            var group = new ManageUserGroupsViewModel();

            var groups = _context.Groups.Where(x => x.Id == Guid.Parse(model.Area)).ToList();

            group.GroupName = groups.FirstOrDefault().Name;

            ListGroups.Add(group);

            await _userManager.OverrideGroups(_context, user, ListGroups.Select(x => x.GroupName));

            var ListRoles = new List<ManageUserRolesViewModel>();

            var Role = new ManageUserRolesViewModel();

            var roles = _roleManager.Roles.Where(x => x.Id == model.Role).ToList();

            Role.RoleName = roles.FirstOrDefault().Name;

            ListRoles.Add(Role);

            await _userManager.AddToRolesAsync(user, ListRoles.Select(x => x.RoleName));

            if (result.Succeeded)
            {
                return RedirectToAction("Users", "Setup").WithSuccess("El Colaborador se registro satisfactoriamente");

            }
            else
            {
                return RedirectToAction("Users", "Setup").WithError("El Colaborador no se registro satisfactoriamente");

            }

        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var userid = _userManager.FindByIdAsync(id.ToString()).Result;

            var user = new UserViewModel()
            {
                UserId = userid.Id,
                FirstName = userid.FirstName,
                LastName = userid.LastName,
                UserName = userid.UserName,
                Email = userid.Email,
                City = userid.CityId.ToString(),
                Phone = userid.PhoneNumber,
                IsActive = userid.IsActive,

            };

            var groups = _context.Groups.ToList();
            foreach (var group in groups)
            {
                var userGroupViewModel = new ManageUserGroupsViewModel
                {
                    GroupId = group.Id,
                    GroupName = group.Name
                };

                if (await _userManager.IsInGroupAsync(_context, userid, group.Name))
                {
                    user.Area = userGroupViewModel.GroupId.ToString();
                }

            }

            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in await _roleManager.Roles.ToListAsync())
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(userid, role.Name))
                {
                    user.Role = userRolesViewModel.RoleId.ToString();
                }
            }

                return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var city = user.CityId;
            var phone = user.PhoneNumber;
            if (model.FirstName != firstName)
            {
                user.FirstName = model.FirstName;
                await _userManager.UpdateAsync(user);
            }
            if (model.LastName != lastName)
            {
                user.LastName = model.LastName;
                await _userManager.UpdateAsync(user);
            }
            if (Guid.Parse(model.City) != city)
            {
                user.CityId = Guid.Parse(model.City);
                await _userManager.UpdateAsync(user);
            }
            if (model.Phone != phone)
            {
                user.PhoneNumber = model.Phone;
                await _userManager.UpdateAsync(user);
            }

            var ListGroups = new List<ManageUserGroupsViewModel>();

            var group = new ManageUserGroupsViewModel();

            var groups = _context.Groups.Where(x => x.Id == Guid.Parse(model.Area)).ToList();

            group.GroupName = groups.FirstOrDefault().Name;

            ListGroups.Add(group);

            await _userManager.OverrideGroups(_context, user, ListGroups.Select(x => x.GroupName));

            var ListRoles = new List<ManageUserRolesViewModel>();

            var Role = new ManageUserRolesViewModel();

            var roles = _roleManager.Roles.Where(x => x.Id == model.Role).ToList();

            Role.RoleName = roles.FirstOrDefault().Name;

            ListRoles.Add(Role);

            await _userManager.AddToRolesAsync(user, ListRoles.Select(x => x.RoleName));


            return RedirectToAction("Users", "Setup").WithSuccess("El Colaborador se edito satisfactoriamente");

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

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
