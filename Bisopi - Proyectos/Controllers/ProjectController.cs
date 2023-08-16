using Bisopi___Proyectos.Alerts;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ModelsTemps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bisopi___Proyectos.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProjectController(DataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = new Project();
            model.ProjectID = Guid.NewGuid();

            return View(model);
        }

        public IActionResult Edit(Guid id)
        {
            var model = _context.Projects.Where(x => x.ProjectID == id).FirstOrDefault();

            var details = _context.Milestones.Where(x => x.ProjectID == model.ProjectID).ToList();

            foreach (var item in details)
            {
                var detailsCheck = _context.MilestonesTemps.Where(x => x.MilestoneTempID == item.MilestoneID).FirstOrDefault();

                if (detailsCheck == null)
                {
                    var newDetail = new MilestoneTemp();
                    newDetail.DealID = item.DealID;
                    newDetail.LeadID = item.LeadID;
                    newDetail.ProjectID = item.ProjectID;
                    newDetail.MilestoneTempID = item.MilestoneID;
                    newDetail.MilestoneDate = item.MilestoneDate;
                    newDetail.CurrencyID = item.CurrencyID;
                    newDetail.Percentage = item.Percentage;
                    newDetail.Value = item.Value;
                    newDetail.MilestoneNumber = item.MilestoneNumber;
                    newDetail.IsItChangeControl = item.IsItChangeControl;
                    newDetail.Comment = item.Comment;
                    newDetail.IsActive = item.IsActive;
                    newDetail.Created = item.Created;
                    newDetail.CreatedBy = item.CreatedBy;
                    newDetail.Modified = item.Modified;
                    newDetail.ModifiedBy = item.ModifiedBy;

                    _context.Add(newDetail);
                }

            }

            _context.SaveChanges();

            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            var model = _context.Projects.Where(x => x.ProjectID == id).FirstOrDefault();

            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;
            model.IsActive = false;

            _context.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Index", "Project").WithSuccess("El registro ha sido eliminado");
        }

        [HttpPost]
        public IActionResult Create(Project model)
        {
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified= DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            var details = _context.MilestonesTemps.Where(x => x.ProjectID == model.ProjectID).ToList();

            foreach (var item in details)
            {
                var newDetail = new Milestone();
                newDetail.DealID = item.DealID;
                newDetail.LeadID = item.LeadID;
                newDetail.ProjectID = item.ProjectID;
                newDetail.MilestoneID = item.MilestoneTempID;
                newDetail.MilestoneDate = item.MilestoneDate;
                newDetail.CurrencyID = item.CurrencyID;
                newDetail.Percentage = item.Percentage;
                newDetail.Value = item.Value;
                newDetail.MilestoneNumber = item.MilestoneNumber;
                newDetail.IsItChangeControl = item.IsItChangeControl;
                newDetail.Comment = item.Comment;
                newDetail.IsActive = item.IsActive;
                newDetail.Created = item.Created;
                newDetail.CreatedBy = item.CreatedBy;
                newDetail.Modified = item.Modified;
                newDetail.ModifiedBy = item.ModifiedBy;

                _context.Add(newDetail);
            }

            _context.MilestonesTemps.RemoveRange(details);

            _context.Add(model);
            _context.SaveChanges();


            return RedirectToAction("Index","Project").WithSuccess("El registro ha sido exitoso");
        }

        [HttpPost]
        public IActionResult Edit(Project model)
        {
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            _context.Update(model);
            _context.SaveChanges();


            return RedirectToAction("Index", "Project").WithSuccess("El registro ha sido actualizado exitosamente");
        }

        [HttpGet]
        public async Task<IActionResult> Task(Guid id)
        {
            Project? project = await _context.Projects
                .Include(x=> x.Client)
                .Include(x=> x.Country)
                .Include(x=> x.ProjectStatus)
                .Include(x=> x.ProjectType)
                .Include(x=> x.Currency)
                .FirstOrDefaultAsync(x=> x.ProjectID == id);
            if (project == null)
                return RedirectToAction(nameof(Index));
            var leader = await _userManager.FindByIdAsync(project.LeaderID.ToString());
            if (leader != null)
            {
                if(leader.NormalizedUserName != null)
                    project.LeaderName = leader.NormalizedUserName;
            }
            var projectManager = await _userManager.FindByIdAsync(project.ProjectManagerID.ToString());
            if (projectManager != null)
            {
                if(projectManager.NormalizedUserName != null)
                    project.ProjectManagerName = projectManager.NormalizedUserName;
            }
            ProjectTask model = new()
            {
                Project = project
            };
            var status = await _context.ProjectTaskStatus.Where(x => x.StatusName.ToLower().Contains("activa")).FirstOrDefaultAsync();
            if (status != null)
            {
                TempData["Status"] = status.ProjectTaskStatusID;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Task(ProjectTask model)
        {

            return Ok();
        }
    }
}
