using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class ProjectTracking
    {
        [Key]
        public Guid ProjectTrackingID { get; set; }

        [Display(Name = "Proyecto")]
        public Guid? ProjectID { get; set; }

        [Display(Name = "Fecha Seguimiento")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime?  MonitoringDate { get; set; }

        [Display(Name = "Porcentaje Planeado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? PlannedPercentage { get; set; }

        [Display(Name = "Porcentaje Real")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? RealPercentage { get; set; }

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
        public DateTime Created { get; set; }

        [Display(Name = "Modificado por")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? ModifiedBy { get; set; }

        [Display(Name = "Modificado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Modified { get; set; }

        [NotMapped]
        public Project? Project { get; set; }
    }
}
