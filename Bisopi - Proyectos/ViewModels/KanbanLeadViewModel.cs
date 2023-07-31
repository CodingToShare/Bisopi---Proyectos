using Bisopi___Proyectos.Models;

namespace Bisopi___Proyectos.ViewModels
{
    public class KanbanLeadViewModel
    {
        public string Status { get; set; }
        public string Color { get; set; }
        public IEnumerable<Lead> LeadsIEnumerable { get; set; }
        public List<QuoteStatus> QuotesStatus { get; set; }
        public List<Lead> Leads { get; set; }
    }
}
