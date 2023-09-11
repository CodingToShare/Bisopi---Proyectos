using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bisopi___Proyectos.ModelsTemps
{
    public class ResourcePlanningTemp
    {
        [Key]
        public Guid ResourcePlanningTempID { get; set; }

        [Display(Name = "Proyecto")]
        public Guid? ProjectID { get; set; }

        [Display(Name = "Deal")]
        public Guid? DealID { get; set; }

        [Display(Name = "Lead")]
        public Guid? LeadID { get; set; }

        [Display(Name = "Colaborador")]
        public Guid? ResourceID { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? PositionID { get; set; }

        [Display(Name = "Horas Planificadas")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? PlannedHours { get; set; }

        [Display(Name = "Tarifa")]
        public double? Fee { get; set; }

        [Display(Name = "Costos")]
        public double? Cost { get; set; }

        [Display(Name = "Horas Etc")]
        public double? EtcHour { get; set; }

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
    }
}
