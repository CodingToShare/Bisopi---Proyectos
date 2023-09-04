using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class Programming
    {
        [Key]
        public Guid ProgrammingID { get; set; }

        [Display(Name = "Proyecto")]
        public Guid? ProjectID { get; set; }

        [Display(Name = "Recurso")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? ResourceID { get; set; }

        [NotMapped]
        public string ResourceName { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? ResourcePositionID { get; set; }

        [Display(Name = "Tipo Actividad")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? ActivityTypeID { get; set; }

        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Fecha Fin")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "% Asignacion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? AllocationPercentage  { get; set; }

        [Display(Name = "Comentarios")]
        [Column(TypeName = "varchar(1000)")]
        public string? Comments { get; set; }

        [Display(Name = "¿Activo?")]
        public bool IsActive { get; set; }

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
