using Bisopi___Proyectos.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bisopi___Proyectos.Controllers
{
    [Route("api/kanban")]
    [ApiController]
    public class KanbanController : ControllerBase
    {
        private DataContext _context;

        public KanbanController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("updateStatusLeads")]
        public IActionResult UpdateStatusLeads(string id, string status)
        {
            status = status.Trim();
            var leadID = Guid.Parse(id);
            var statusLead = _context.QuotesStatus.Where(x => x.QuoteStatusName == status).FirstOrDefault();

            var lead = _context.Leads.Where(x => x.LeadID == leadID).FirstOrDefault();
            lead.QuoteStatusID = statusLead.QuoteStatusID;

            _context.Update(lead);
            _context.SaveChanges();

            return Ok(new { Message = "Estado actualizado exitosamente" });
        }

        [HttpGet("updateStatusDeals")]
        public IActionResult UpdateStatusDeals(string id, string status)
        {
            status = status.Trim();
            var dealID = Guid.Parse(id);
            var statusDeal = _context.ProposalsStatus.Where(x => x.ProposalStatusName == status).FirstOrDefault();

            var deal = _context.Deals.Where(x => x.DealID == dealID).FirstOrDefault();
            deal.ProposalStatusID = statusDeal.ProposalStatusID;

            _context.Update(deal);
            _context.SaveChanges();

            return Ok(new { Message = "Estado actualizado exitosamente" });
        }

    }
}
