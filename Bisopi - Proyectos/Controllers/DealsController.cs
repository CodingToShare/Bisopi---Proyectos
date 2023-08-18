using Bisopi___Proyectos.Alerts;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ModelsTemps;
using Bisopi___Proyectos.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bisopi___Proyectos.Controllers
{
    public class DealsController : Controller
    {
        private DataContext _context;

        public DealsController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var modelProposalsStatus = _context.ProposalsStatus.Where(x => x.IsActive && x.Visible).ToList();
            var modelDeals = _context.Deals.Where(x => x.IsActive).ToList();

            var model = new KanbanDealViewModel();

            model.ProposalsStatus = modelProposalsStatus;
            model.Deals = modelDeals;

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new Deal(); 
            model.DealID = Guid.NewGuid();

            return View(model);
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Edit(Guid id)
        {
            var model = _context.Deals.Where(x => x.DealID == id).FirstOrDefault();

            var details = _context.Milestones.Where(x => x.DealID == model.DealID).ToList();

            foreach (var item in details)
            {
                var detailsCheck = _context.MilestonesTemps.Where(x => x.MilestoneTempID == item.MilestoneID).FirstOrDefault();

                if (detailsCheck == null)
                {
                    var newDetail = new MilestoneTemp();
                    newDetail.DealID = item.DealID;
                    newDetail.LeadID = item.LeadID;
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

            var detailsResource = _context.ResourcesPlannings.Where(x => x.DealID == model.DealID).ToList();

            foreach (var item in detailsResource)
            {
                var detailsCheck = _context.ResourcesPlanningsTemps.Where(x => x.ResourcePlanningTempID == item.ResourcePlanningID).FirstOrDefault();

                if (detailsCheck == null)
                {
                    var newDetail = new ResourcePlanningTemp();
                    newDetail.DealID = item.DealID;
                    newDetail.LeadID = item.LeadID;
                    newDetail.ResourcePlanningTempID = item.ResourcePlanningID;
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

            }

            _context.SaveChanges();

            return View(model);
        }

        public IActionResult DealToProject(Guid id)
        {
            var model = _context.Deals.Where(x => x.DealID == id).FirstOrDefault();

            var modelProject = new Project();

            modelProject.ProjectID = Guid.NewGuid();
            modelProject.DealID = model.DealID;
            modelProject.ProjectName = model.DealName;
            modelProject.ClientID = model.ClientID;
            modelProject.CustomerManager = model.ResponsibleClient;
            modelProject.CurrencyID = model.CurrencyID;
            modelProject.ProjectValue = model.LeadValue;
            modelProject.Justification = model.Comments;

            var details = _context.Milestones.Where(x => x.DealID == model.DealID).ToList();

            foreach (var item in details)
            {
                var detailsCheck = _context.MilestonesTemps.Where(x => x.MilestoneTempID == item.MilestoneID).FirstOrDefault();

                if (detailsCheck == null)
                {
                    var newDetail = new MilestoneTemp();
                    newDetail.ProjectID = modelProject.ProjectID;
                    newDetail.DealID = modelProject.DealID;
                    newDetail.LeadID = model.LeadID;
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
                else
                {
                    detailsCheck.ProjectID = modelProject.ProjectID;
                }

            }

            var detailsResources = _context.ResourcesPlannings.Where(x => x.DealID == model.DealID).ToList();

            foreach (var item in detailsResources)
            {
                var detailsCheckResource = _context.ResourcesPlanningsTemps.Where(x => x.ResourcePlanningTempID == item.ResourcePlanningID).FirstOrDefault();

                if (detailsCheckResource == null)
                {
                    var newDetail = new ResourcePlanningTemp();
                    newDetail.ProjectID = item.ProjectID;
                    newDetail.DealID = item.DealID;
                    newDetail.LeadID = item.LeadID;
                    newDetail.ResourcePlanningTempID = item.ResourcePlanningID;
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
                else
                {
                    detailsCheckResource.DealID = modelProject.DealID;
                }

            }

            _context.SaveChanges();

            return View(modelProject);
        }

        public IActionResult Delete(Guid id)
        {
            var model = _context.Deals.Where(x => x.DealID == id).FirstOrDefault();

            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;
            model.IsActive = false;

            _context.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Index", "Deals").WithSuccess("El registro ha sido eliminado");
        }

        [HttpPost]
        public IActionResult Create(Deal model)
        {
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            var details = _context.MilestonesTemps.Where(x => x.DealID == model.DealID).ToList();

            foreach (var item in details)
            {
                var newDetail = new Milestone();
                newDetail.DealID = item.DealID;
                newDetail.LeadID = item.LeadID;
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

            var detailsResourcePlaning = _context.ResourcesPlanningsTemps.Where(x => x.DealID == model.DealID).ToList();

            foreach (var item in detailsResourcePlaning)
            {
                var newDetail = new ResourcePlanning();
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


            return RedirectToAction("Index", "Deals").WithSuccess("El registro ha sido exitoso");
        }

        [HttpPost]
        public IActionResult Edit(Deal model)
        {
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            var detailsOld = _context.Milestones.Where(x => x.DealID == model.DealID).ToList();

            _context.Milestones.RemoveRange(detailsOld);

            var details = _context.MilestonesTemps.Where(x => x.DealID == model.DealID).ToList();

            foreach (var item in details)
            {
                var newDetail = new Milestone();
                newDetail.DealID = item.DealID;
                newDetail.LeadID = item.LeadID;
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

            var detailsOldResource = _context.ResourcesPlannings.Where(x => x.DealID == model.DealID).ToList();

            _context.ResourcesPlannings.RemoveRange(detailsOldResource);

            var detailsResourcePlaning = _context.ResourcesPlanningsTemps.Where(x => x.DealID == model.DealID).ToList();

            foreach (var item in detailsResourcePlaning)
            {
                var newDetail = new ResourcePlanning();
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

            _context.Update(model);
            _context.SaveChanges();


            return RedirectToAction("Index", "Deals").WithSuccess("El registro ha sido actualizado exitosamente");
        }

        [HttpPost]
        public IActionResult DealToProject(Project model)
        {
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            var detailsOld = _context.Milestones.Where(x => x.DealID == model.DealID).ToList();

            _context.Milestones.RemoveRange(detailsOld);

            var details = _context.MilestonesTemps.Where(x => x.DealID == model.DealID).ToList();

            if (details.Count == 0)
            {
                return RedirectToAction("DealToProject", "Deals").WithError("Tiene que registrar un Hito por lo menos");
            }

            foreach (var item in details)
            {
                var newDetail = new Milestone();
                newDetail.LeadID = item.LeadID;
                newDetail.DealID = model.DealID;
                newDetail.ProjectID = model.ProjectID;
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

            var detailsOldResource = _context.ResourcesPlannings.Where(x => x.DealID == model.DealID).ToList();

            _context.ResourcesPlannings.RemoveRange(detailsOldResource);

            var detailsResources = _context.ResourcesPlanningsTemps.Where(x => x.DealID == model.DealID).ToList();

            foreach (var item in detailsResources)
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

            _context.ResourcesPlanningsTemps.RemoveRange(detailsResources);

            _context.Add(model);

            var modelLead = _context.Deals.Where(x => x.DealID == model.DealID).FirstOrDefault();

            modelLead.IsActive = false;

            _context.Update(modelLead);

            _context.SaveChanges();

            return RedirectToAction("Index", "Project").WithSuccess("El registro ha sido exitoso");
        }
    }
}
