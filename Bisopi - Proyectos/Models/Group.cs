using System.ComponentModel.DataAnnotations;

namespace Bisopi___Proyectos.Models
{
    public class Group
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
