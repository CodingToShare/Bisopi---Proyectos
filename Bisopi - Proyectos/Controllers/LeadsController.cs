using Bisopi___Proyectos.Alerts;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bisopi___Proyectos.Controllers
{
    public class LeadsController : Controller
    {
        private DataContext _context;

        public LeadsController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var modelQuotesStatus = _context.QuotesStatus.Where(x => x.IsActive && x.Visible).ToList();
            var modelLeads = _context.Leads.Where(x => x.IsActive).ToList();

            var model = new KanbanLeadViewModel();

            model.QuotesStatus = modelQuotesStatus;
            model.Leads = modelLeads;

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Edit(Guid id)
        {
            var model = _context.Leads.Where(x => x.LeadID == id).FirstOrDefault();

            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            var model = _context.Leads.Where(x => x.LeadID == id).FirstOrDefault();

            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;
            model.IsActive = false;

            _context.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Index", "Leads").WithSuccess("El registro ha sido eliminado");
        }

        [HttpPost]
        public IActionResult Create(Lead model)
        {
            model.LeadID = Guid.NewGuid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            _context.Add(model);
            _context.SaveChanges();


            return RedirectToAction("Index", "Leads").WithSuccess("El registro ha sido exitoso");
        }

        [HttpPost]
        public IActionResult Edit(Lead model)
        {
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            _context.Update(model);
            _context.SaveChanges();


            return RedirectToAction("Index", "Leads").WithSuccess("El registro ha sido actualizado exitosamente");
        }

    }
}
