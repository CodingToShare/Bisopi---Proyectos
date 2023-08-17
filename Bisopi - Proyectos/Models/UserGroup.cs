using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    [PrimaryKey(nameof(UserId), nameof(GroupId))]
    public class UserGroup
    {
        [Column(TypeName = "nvarchar")]
        [MaxLength(450)]
        [Required]
        public string UserId { get; set; }

        [Required]
        public Guid GroupId { get; set; }
    }
}
