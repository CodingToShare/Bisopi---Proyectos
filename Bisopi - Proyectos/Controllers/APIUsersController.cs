﻿using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Extensions;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ViewModels;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bisopi___Proyectos.Controllers
{
    public class APIUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public APIUsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
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
