using System.ComponentModel.DataAnnotations;

namespace Bisopi___Proyectos.Models
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(5)]
        public string Abbreviation { get; set; } = string.Empty;
    }
}
