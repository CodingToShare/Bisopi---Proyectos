using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class TaskGroup
    {
        [Key]
        public Guid TaskGroupID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Grupo tarea")]
        public string Name { get; set; }
    
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Abreviatura")]
        public string Abbreviation { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Orden")]
        public int Order { get; set; }

        [Display(Name = "¿Activo?")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Creado por")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? CreatedBy { get; set; }

        [Display(Name = "Creado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Created { get; set; }

        [Display(Name = "Modificado por")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? ModifiedBy { get; set; }

        [Display(Name = "Modificado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Modified { get; set; }
    }
}
