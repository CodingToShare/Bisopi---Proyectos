using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class ResourcePlanningReal
    {
        [Key]
        public Guid ResourcePlanningRealID { get; set; }

        [Display(Name = "Proyecto")]
        public Guid? ProjectID { get; set; }

        [Display(Name = "Fecha Análisis")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? DateAnalysis { get; set; }

        [Display(Name = "Colaborador")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? ResourceID { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? PositionID { get; set; }

        [Display(Name = "Horas")]
        public double? PlannedHours { get; set; }

        [Display(Name = "% Completado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? PercentComplete { get; set; }

        [Display(Name = "% Esperado")]
        public double? ExpectedPercentage { get; set; }

        [Display(Name = "¿Activo?")]
        public bool IsActive { get; set; }

        [Display(Name = "Creado por")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string CreatedBy { get; set; }

        [Display(Name = "Creado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        [Display(Name = "Modificado por")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modificado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Modified { get; set; }

        [NotMapped]
        public Project Project { get; set; }
    }
}
