using System.ComponentModel.DataAnnotations;

namespace Bisopi___Proyectos.Models
{
    public class AllowedView
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
