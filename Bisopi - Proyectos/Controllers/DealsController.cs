using Bisopi___Proyectos.Alerts;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
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
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Edit(Guid id)
        {
            var model = _context.Deals.Where(x => x.DealID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Deal model)
        {
            model.DealID = Guid.NewGuid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            _context.Add(model);
            _context.SaveChanges();


            return RedirectToAction("Index", "Deals").WithSuccess("El registro ha sido exitoso");
        }

        [HttpPost]
        public IActionResult Edit(Deal model)
        {
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            _context.Update(model);
            _context.SaveChanges();


            return RedirectToAction("Index", "Deals").WithSuccess("El registro ha sido actualizado exitosamente");
        }
    }
}
