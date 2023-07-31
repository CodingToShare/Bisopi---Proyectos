using Bisopi___Proyectos.Models;

namespace Bisopi___Proyectos.ViewModels
{
    public class KanbanDealViewModel
    {
        public string Status { get; set; }
        public string Color { get; set; }
        public IEnumerable<Deal> DealsIEnumerable { get; set; }
        public List<ProposalStatus> ProposalsStatus { get; set; }
        public List<Deal> Deals { get; set; }
    }
}
