using Microsoft.EntityFrameworkCore;

namespace Bisopi___Proyectos.Models
{
    [PrimaryKey(nameof(GroupId), nameof(AllowedViewId))]
    public class AllowedViewForGroup
    {
        public Guid GroupId { get; set; }

        public Guid AllowedViewId { get; set; }
        public virtual AllowedView? AllowedView { get; set; }
    }
}
