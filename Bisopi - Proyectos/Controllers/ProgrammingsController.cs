using Bisopi___Proyectos.Calendar;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bisopi___Proyectos.Controllers
{
    public class ProgrammingsController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgrammingsController(DataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult ProjectMonitoring()
        {
            var programmings = _context.Programmings.Where(x => x.IsActive).ToList();

            foreach (var progra in programmings)
            {
                var resource = _userManager.FindByIdAsync(progra.ResourceID.ToString());

                progra.ResourceName = resource.Result.FirstName + " " + resource.Result.LastName;
            }

            var projects = _context.Projects.Where(x => x.IsActive).ToList();

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

            Models.Project? project = _context.Projects
                .Include(x => x.Client)
                .Include(x => x.Country)
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectType)
                .Include(x => x.Currency)
                .FirstOrDefault();

            var actualTime = project.StartDate.Value;

            var months = new List<Month>();

            while (actualTime <= project.EstimatedDeliveryDate)
            {
                months.Add(new Month(actualTime.Month, actualTime.Year));
                actualTime = actualTime.AddMonths(1);
            }

            var programmingViewModel = new ProgrammingViewModel()
            {
                ProgrammingsList = programmings,
                Project = project,
                Months = months,
                ProjectsList = projects,
                UsersDataList = resultList
            };


            return View(programmingViewModel);
        }

        public IActionResult ResourceMonitoring()
        {
            var programmings = _context.Programmings.Where(x => x.IsActive).ToList();

            foreach (var progra in programmings)
            {
                var resource = _userManager.FindByIdAsync(progra.ResourceID.ToString());

                progra.ResourceName = resource.Result.FirstName + " " + resource.Result.LastName;
            }

            var projects = _context.Projects.Where(x => x.IsActive).ToList();

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

            Models.Project? project = _context.Projects
                .Include(x => x.Client)
                .Include(x => x.Country)
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectType)
                .Include(x => x.Currency)
                .FirstOrDefault();

            var actualTime = project.StartDate.Value;

            var months = new List<Month>();

            while (actualTime <= project.EstimatedDeliveryDate)
            {
                months.Add(new Month(actualTime.Month, actualTime.Year));
                actualTime = actualTime.AddMonths(1);
            }

            var programmingViewModel = new ProgrammingViewModel()
            {
                ProgrammingsList = programmings,
                Project = project,
                Months = months,
                ProjectsList = projects,
                UsersDataList = resultList
            };


            return View(programmingViewModel);
        }
    }
}
