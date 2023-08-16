﻿using Azure.Core;
using Bisopi___Proyectos.Alerts;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Bisopi___Proyectos.ModelsTemps;
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
            var model = new Lead();
            model.LeadID = Guid.NewGuid();

            return View(model);
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Edit(Guid id)
        {
            var model = _context.Leads.Where(x => x.LeadID == id).FirstOrDefault();

            var details = _context.Milestones.Where(x => x.LeadID == model.LeadID).ToList();

            foreach (var item in details)
            {
                var detailsCheck = _context.MilestonesTemps.Where(x => x.MilestoneTempID == item.MilestoneID).FirstOrDefault();

                if(detailsCheck == null)
                {
                    var newDetail = new MilestoneTemp();
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

            _context.SaveChanges();

            return View(model);
        }

        public IActionResult LeadToDeal(Guid id)
        {
            var model = _context.Leads.Where(x => x.LeadID == id).FirstOrDefault();

            var modelDeal = new Deal();

            modelDeal.DealID = Guid.NewGuid();
            modelDeal.LeadID = model.LeadID;
            modelDeal.DealName = model.LeadName;
            modelDeal.ClientID = model.ClientID;
            modelDeal.ResponsibleClient = model.ResponsibleClient;
            modelDeal.CurrencyID = model.CurrencyID;
            modelDeal.LeadValue = model.LeadValue;
            modelDeal.Comments = model.Comments;

            var details = _context.Milestones.Where(x => x.LeadID == model.LeadID).ToList();

            foreach (var item in details)
            {
                var detailsCheck = _context.MilestonesTemps.Where(x => x.MilestoneTempID == item.MilestoneID).FirstOrDefault();

                if (detailsCheck == null)
                {
                    var newDetail = new MilestoneTemp();
                    newDetail.DealID = modelDeal.DealID;
                    newDetail.LeadID = modelDeal.LeadID;
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
                    detailsCheck.DealID = modelDeal.DealID;
                }

            }

            _context.SaveChanges();

            return View(modelDeal);
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
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            var details = _context.MilestonesTemps.Where(x => x.LeadID == model.LeadID).ToList();

            foreach (var item in details)
            {
                var newDetail = new Milestone();
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

            _context.Add(model);
            _context.SaveChanges();


            return RedirectToAction("Index", "Leads").WithSuccess("El registro ha sido exitoso");
        }

        [HttpPost]
        public IActionResult Edit(Lead model)
        {
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            var detailsOld = _context.Milestones.Where(x => x.LeadID == model.LeadID).ToList();

            _context.Milestones.RemoveRange(detailsOld);

            var details = _context.MilestonesTemps.Where(x => x.LeadID == model.LeadID).ToList();

            foreach (var item in details)
            {
                var newDetail = new Milestone();
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

            _context.Update(model);
            _context.SaveChanges();


            return RedirectToAction("Index", "Leads").WithSuccess("El registro ha sido actualizado exitosamente");
        }

        [HttpPost]
        public IActionResult LeadToDeal(Deal model)
        {
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            var detailsOld = _context.Milestones.Where(x => x.LeadID == model.LeadID).ToList();

            _context.Milestones.RemoveRange(detailsOld);

            var details = _context.MilestonesTemps.Where(x => x.LeadID == model.LeadID).ToList();

            foreach (var item in details)
            {
                var newDetail = new Milestone();
                newDetail.LeadID = item.LeadID;
                newDetail.DealID = model.DealID;
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

            var modelLead = _context.Leads.Where(x => x.LeadID == model.LeadID).FirstOrDefault();

            modelLead.IsActive = false;

            _context.Update(modelLead); 

            _context.SaveChanges();

            return RedirectToAction("Index", "Deals").WithSuccess("El registro ha sido exitoso");
        }
    }
}