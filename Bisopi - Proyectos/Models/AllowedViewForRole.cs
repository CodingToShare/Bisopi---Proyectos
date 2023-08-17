using Microsoft.EntityFrameworkCore;

namespace Bisopi___Proyectos.Models
{
    [PrimaryKey(nameof(RoleId), nameof(AllowedViewId))]
    public class AllowedViewForRole
    {
        public Guid RoleId { get; set; }

        public Guid AllowedViewId { get; set; }
        public virtual AllowedView? AllowedView { get; set; }
    }
}
