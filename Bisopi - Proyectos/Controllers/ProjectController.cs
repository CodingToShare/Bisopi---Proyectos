using Bisopi___Proyectos.Alerts;
using Bisopi___Proyectos.Calendar;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ModelsTemps;
using Bisopi___Proyectos.ViewModels;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Project = Bisopi___Proyectos.Models.Project;

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
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            var details = _context.MilestonesTemps.Where(x => x.ProjectID == model.ProjectID).ToList();

            if (details.Count == 0)
            {
                return RedirectToAction("Create", "Project").WithError("Tiene que registrar un Hito por lo menos");
            }

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

                var modelBill = new Bill();
                modelBill.BillID = new Guid();
                modelBill.MilestoneID = item.MilestoneTempID;
                modelBill.StatusBill = Bisopi___Proyectos.Enums.StatusBill.PendingBilling;
                modelBill.IsActive = item.IsActive;
                modelBill.Created = DateTime.UtcNow.AddHours(-5);
                modelBill.CreatedBy = User.Identity.Name;
                modelBill.Modified = DateTime.UtcNow.AddHours(-5);
                modelBill.ModifiedBy = User.Identity.Name;

                _context.Add(modelBill);

                var modelInvoiceReport = new InvoiceReport();
                modelInvoiceReport.InvoiceReportID = new Guid();
                modelInvoiceReport.BillID = modelBill.BillID;
                modelInvoiceReport.MilestoneID = item.MilestoneTempID;
                modelInvoiceReport.ProjectID = model.ProjectID;
                modelInvoiceReport.ProjectName = model.ProjectName;
                modelInvoiceReport.CountryID = model.CountryID;
                modelInvoiceReport.ClientID = model.ClientID;
                modelInvoiceReport.ProjectStatusID = model.ProjectStatusID;
                modelInvoiceReport.IsItChangeControl = item.IsItChangeControl;
                modelInvoiceReport.MilestoneNumber = item.MilestoneNumber;
                modelInvoiceReport.MilestoneDate = item.MilestoneDate;
                modelInvoiceReport.StatusBill = modelBill.StatusBill;
                modelInvoiceReport.CurrencyID = model.CurrencyID;
                modelInvoiceReport.Value = item.Value;
                modelInvoiceReport.IsActive = item.IsActive;
                modelInvoiceReport.Created = DateTime.UtcNow.AddHours(-5);
                modelInvoiceReport.CreatedBy = User.Identity.Name;
                modelInvoiceReport.Modified = DateTime.UtcNow.AddHours(-5);
                modelInvoiceReport.ModifiedBy = User.Identity.Name;

                _context.Add(modelInvoiceReport);
            }

            _context.MilestonesTemps.RemoveRange(details);

            var detailsResourcePlaning = _context.ResourcesPlanningsTemps.Where(x => x.ProjectID == model.ProjectID).ToList();

            foreach (var item in detailsResourcePlaning)
            {
                var newDetail = new ResourcePlanning();
                newDetail.ProjectID = item.ProjectID;
                newDetail.DealID = item.DealID;
                newDetail.LeadID = item.LeadID;
                newDetail.ResourcePlanningID = item.ResourcePlanningTempID;
                newDetail.ResourceID = item.ResourceID;
                newDetail.PositionID = item.PositionID;
                newDetail.PlannedHours = item.PlannedHours;
                newDetail.EtcHour = item.EtcHour;
                newDetail.IsActive = item.IsActive;
                newDetail.Created = item.Created;
                newDetail.CreatedBy = item.CreatedBy;
                newDetail.Modified = item.Modified;
                newDetail.ModifiedBy = item.ModifiedBy;

                _context.Add(newDetail);
            }

            _context.ResourcesPlanningsTemps.RemoveRange(detailsResourcePlaning);

            _context.Add(model);
            _context.SaveChanges();


            return RedirectToAction("Index", "Project").WithSuccess("El registro ha sido exitoso");
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
                .Include(x => x.Client)
                .Include(x => x.Country)
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectType)
                .Include(x => x.Currency)
                .FirstOrDefaultAsync(x => x.ProjectID == id);
            if (project == null)
                return RedirectToAction(nameof(Index));
            var leader = await _userManager.FindByIdAsync(project.LeaderID.ToString());
            if (leader != null)
            {
                if (leader.NormalizedUserName != null)
                    project.LeaderName = leader.NormalizedUserName;
            }
            var projectManager = await _userManager.FindByIdAsync(project.ProjectManagerID.ToString());
            if (projectManager != null)
            {
                if (projectManager.NormalizedUserName != null)
                    project.ProjectManagerName = projectManager.NormalizedUserName;
            }
            ProjectTask model = new()
            {
                Project = project,
                ProjectID = project.ProjectID
            };
            var status = await _context.ProjectTaskStatus.Where(x => x.StatusName.ToLower().Contains("activa")).FirstOrDefaultAsync();
            if (status != null)
            {
                model.TaskStatusID = status.ProjectTaskStatusID;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Task(ProjectTask model)
        {
            int estimateTime = model.EstimateTime * 3600 + model.ExecutionTime * 60;
            model.EstimateTime = estimateTime;
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            await _context.ProjectTask.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Task", "Project").WithSuccess("El registro ha sido exitoso");
        }

        [HttpGet]
        public async Task<IActionResult> Milestone(Guid id)
        {
            Project? project = await _context.Projects
                .Include(x => x.Client)
                .Include(x => x.Country)
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectType)
                .Include(x => x.Currency)
                .FirstOrDefaultAsync(x => x.ProjectID == id);

            if (project == null)
                return RedirectToAction(nameof(Index));

            var leader = await _userManager.FindByIdAsync(project.LeaderID.ToString());

            if (leader != null)
            {
                if (leader.NormalizedUserName != null)
                    project.LeaderName = leader.NormalizedUserName;
            }

            var projectManager = await _userManager.FindByIdAsync(project.ProjectManagerID.ToString());

            if (projectManager != null)
            {
                if (projectManager.NormalizedUserName != null)
                    project.ProjectManagerName = projectManager.NormalizedUserName;
            }

            Milestone model = new()
            {
                Project = project,
                ProjectID = id,
                DealID = project.DealID
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Milestone(Milestone model)
        {
            model.MilestoneID = Guid.NewGuid();
            model.Percentage = model.Percentage * 100;
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            _context.Add(model);

            var modelBill = new Bill();
            modelBill.BillID = new Guid();
            modelBill.MilestoneID = model.MilestoneID;
            modelBill.StatusBill = Bisopi___Proyectos.Enums.StatusBill.PendingBilling;
            modelBill.IsActive = true;
            modelBill.Created = DateTime.UtcNow.AddHours(-5);
            modelBill.CreatedBy = User.Identity.Name;
            modelBill.Modified = DateTime.UtcNow.AddHours(-5);
            modelBill.ModifiedBy = User.Identity.Name;

            _context.Add(modelBill);

            var modelProject = _context.Projects.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();

            var modelInvoiceReport = new InvoiceReport();
            modelInvoiceReport.InvoiceReportID = new Guid();
            modelInvoiceReport.BillID = modelBill.BillID;
            modelInvoiceReport.MilestoneID = model.MilestoneID;
            modelInvoiceReport.ProjectID = model.ProjectID;
            modelInvoiceReport.ProjectName = modelProject.ProjectName;
            modelInvoiceReport.CountryID = modelProject.CountryID;
            modelInvoiceReport.ClientID = modelProject.ClientID;
            modelInvoiceReport.ProjectStatusID = modelProject.ProjectStatusID;
            modelInvoiceReport.IsItChangeControl = model.IsItChangeControl;
            modelInvoiceReport.MilestoneNumber = model.MilestoneNumber;
            modelInvoiceReport.MilestoneDate = model.MilestoneDate;
            modelInvoiceReport.StatusBill = modelBill.StatusBill;
            modelInvoiceReport.CurrencyID = model.CurrencyID;
            modelInvoiceReport.Value = model.Value;
            modelInvoiceReport.IsActive = model.IsActive;
            modelInvoiceReport.Created = DateTime.UtcNow.AddHours(-5);
            modelInvoiceReport.CreatedBy = User.Identity.Name;
            modelInvoiceReport.Modified = DateTime.UtcNow.AddHours(-5);
            modelInvoiceReport.ModifiedBy = User.Identity.Name;

            _context.Add(modelInvoiceReport);
            _context.SaveChanges();

            return RedirectToAction("Milestone", "Project").WithSuccess("El registro ha sido exitoso");
        }

        [HttpGet]
        public async Task<IActionResult> Commitment(Guid id)
        {
            Project? project = await _context.Projects
                .Include(x => x.Client)
                .Include(x => x.Country)
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectType)
                .Include(x => x.Currency)
                .FirstOrDefaultAsync(x => x.ProjectID == id);

            if (project == null)
                return RedirectToAction(nameof(Index));

            var leader = await _userManager.FindByIdAsync(project.LeaderID.ToString());

            if (leader != null)
            {
                if (leader.NormalizedUserName != null)
                    project.LeaderName = leader.NormalizedUserName;
            }

            var projectManager = await _userManager.FindByIdAsync(project.ProjectManagerID.ToString());

            if (projectManager != null)
            {
                if (projectManager.NormalizedUserName != null)
                    project.ProjectManagerName = projectManager.NormalizedUserName;
            }

            ProjectCommitment model = new()
            {
                Project = project,
                ProjectID = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Commitment(ProjectCommitment model)
        {
            model.ProjectCommitmentID = Guid.NewGuid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Commitment", "Project").WithSuccess("El registro ha sido exitoso");
        }

        [HttpGet]
        public async Task<IActionResult> ResourcePlanning(Guid id)
        {
            Models.Project? project = await _context.Projects
                .Include(x => x.Client)
                .Include(x => x.Country)
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectType)
                .Include(x => x.Currency)
                .FirstOrDefaultAsync(x => x.ProjectID == id);

            if (project == null)
                return RedirectToAction(nameof(Index));

            var leader = await _userManager.FindByIdAsync(project.LeaderID.ToString());

            if (leader != null)
            {
                if (leader.NormalizedUserName != null)
                    project.LeaderName = leader.NormalizedUserName;
            }

            var projectManager = await _userManager.FindByIdAsync(project.ProjectManagerID.ToString());

            if (projectManager != null)
            {
                if (projectManager.NormalizedUserName != null)
                    project.ProjectManagerName = projectManager.NormalizedUserName;
            }

            ResourcePlanning modelRP = new()
            {
                Project = project,
                ProjectID = id,
                DealID = project.DealID
            };

            ResourcePlanningReal modelRPR = new()
            {
                Project = project,
                ProjectID = id,
            };

            var model = new ResourcePlanningViewModel()
            {
                ResourcePlannings = modelRP,
                ResourcePlanningReals = modelRPR
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResourcePlanning(ResourcePlanningViewModel modelVM)
        {
            var model = new ResourcePlanning()
            {
                ResourcePlanningID = Guid.NewGuid(),
                ProjectID = modelVM.ResourcePlannings.ProjectID,
                DealID = modelVM.ResourcePlannings.DealID,
                LeadID = modelVM.ResourcePlannings.LeadID,
                ResourceID = modelVM.ResourcePlannings.ResourceID,
                PositionID = modelVM.ResourcePlannings.PositionID,
                PlannedHours = modelVM.ResourcePlannings.PlannedHours,
                EtcHour = modelVM.ResourcePlannings.EtcHour,
                IsActive = true,
                Created = DateTime.UtcNow.AddHours(-5),
                CreatedBy = User.Identity.Name,
                Modified = DateTime.UtcNow.AddHours(-5),
                ModifiedBy = User.Identity.Name
            };

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("ResourcePlanning", "Project").WithSuccess("El registro ha sido exitoso");
        }

        [HttpPost]
        public async Task<IActionResult> ResourcePlanningReal(ResourcePlanningViewModel modelVM)
        {
            var model = new ResourcePlanningReal()
            {
                ResourcePlanningRealID = Guid.NewGuid(),
                ProjectID = modelVM.ResourcePlanningReals.ProjectID,
                DateAnalysis = modelVM.ResourcePlanningReals.DateAnalysis,
                ResourceID = modelVM.ResourcePlanningReals.ResourceID,
                PositionID = modelVM.ResourcePlanningReals.PositionID,
                PlannedHours = modelVM.ResourcePlanningReals.PlannedHours,
                PercentComplete = modelVM.ResourcePlanningReals.PercentComplete,
                ExpectedPercentage = modelVM.ResourcePlanningReals.ExpectedPercentage,
                IsActive = true,
                Created = DateTime.UtcNow.AddHours(-5),
                CreatedBy = User.Identity.Name,
                Modified = DateTime.UtcNow.AddHours(-5),
                ModifiedBy = User.Identity.Name
            };

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("ResourcePlanning", "Project", new { id = modelVM.ResourcePlanningReals.ProjectID }).WithSuccess("El registro ha sido exitoso");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStateBill(Guid id)
        {
            var model = _context.Bills.Where(x => x.BillID == id).FirstOrDefault();

            var modelProject = _context.Milestones.Where(x => x.MilestoneID == model.MilestoneID).FirstOrDefault();

            model.StatusBill = Bisopi___Proyectos.Enums.StatusBill.ApprovedToBill;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            _context.SaveChanges();

            return RedirectToAction("Milestone", "Project", new { id = modelProject.ProjectID }).WithSuccess("La factura ha sido aprovada");
        }

        [HttpGet]
        public async Task<IActionResult> Programming(Guid id)
        {
            var programmings = _context.Programmings.Where(x => x.ProjectID == id && x.IsActive).ToList();

            var programming = new Programming()
            {
                ProjectID = id
            };

            foreach (var progra in programmings)
            {
                var resource = _userManager.FindByIdAsync(progra.ResourceID.ToString());

                progra.ResourceName = resource.Result.FirstName + " " + resource.Result.LastName;
            }

            Models.Project? project = await _context.Projects
                .Include(x => x.Client)
                .Include(x => x.Country)
                .Include(x => x.ProjectStatus)
                .Include(x => x.ProjectType)
                .Include(x => x.Currency)
                .FirstOrDefaultAsync(x => x.ProjectID == id);

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
                Programming = programming,
                Project = project,
                Months = months
            };


            return View(programmingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProgramming(ProgrammingViewModel viewModelProgramming)
        {
            var model = viewModelProgramming.Programming;

            var project = _context.Projects.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();

            if (model.StartDate < project.StartDate || model.EndDate > project.EstimatedDeliveryDate)
            {
                return RedirectToAction("Programming", "Project", new { id = viewModelProgramming.Programming.ProjectID }).WithError("Las fechas estan por fuera del tiempo estimado del proyecto");
            }

            var user = _context.Programmings.Where(x => x.ResourceID == model.ResourceID).FirstOrDefault();

            if (user != null)
            {
                return RedirectToAction("Programming", "Project", new { id = viewModelProgramming.Programming.ProjectID }).WithError("El recurso ya ha sido registrado");
            }

            model.ProgrammingID = new Guid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Programming", "Project", new { id = viewModelProgramming.Programming.ProjectID }).WithSuccess("La programacion ha sido registrada");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProgramming(ProgrammingViewModel viewModelProgramming)
        {
            var model = viewModelProgramming.Programming;

            var modelUpdate = _context.Programmings.Where(x => x.ResourceID == model.ResourceID && x.ProjectID == model.ProjectID).FirstOrDefault();

            modelUpdate.StartDate = model.StartDate;
            modelUpdate.EndDate = model.EndDate;
            modelUpdate.AllocationPercentage = model.AllocationPercentage;
            modelUpdate.ModifiedBy = User.Identity.Name;
            modelUpdate.Modified = DateTime.UtcNow.AddHours(-5);

            _context.SaveChanges();

            return RedirectToAction("Programming", "Project", new { id = viewModelProgramming.Programming.ProjectID }).WithSuccess("La programacion ha sido actualizada");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProgramming(ProgrammingViewModel viewModelProgramming)
        {
            var model = viewModelProgramming.Programming;

            var modelUpdate = _context.Programmings.Where(x => x.ResourceID == model.ResourceID && x.ProjectID == model.ProjectID).FirstOrDefault();

            modelUpdate.IsActive = false;
            modelUpdate.ModifiedBy = User.Identity.Name;
            modelUpdate.Modified = DateTime.UtcNow.AddHours(-5);

            _context.SaveChanges();

            return RedirectToAction("Programming", "Project", new { id = viewModelProgramming.Programming.ProjectID }).WithSuccess("La programacion ha sido eliminada");
        }
    }
}
