using Bisopi___Proyectos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.ModelsTemps
{
    public class MilestoneTemp
    {
        [Key]
        public Guid MilestoneTempID { get; set; }

        [Display(Name = "Proyecto")]
        public Guid? ProjectID { get; set; }

        [Display(Name = "Deal")]
        public Guid? DealID { get; set; }

        [Display(Name = "Lead")]
        public Guid? LeadID { get; set; }

        [Display(Name = "Fecha Hito")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime MilestoneDate { get; set; }

        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? CurrencyID { get; set; }

        [Display(Name = "Porcentaje")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? Percentage { get; set; }

        [Display(Name = "Valor")]
        public double? Value { get; set; }

        [Display(Name = "Nro. Hito")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? MilestoneNumber { get; set; }

        [Display(Name = "¿Es control de Cambio?")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool IsItChangeControl { get; set; }

        [Display(Name = "Horas")]
        public int? Hours { get; set; }

        [Display(Name = "Comentario")]
        [Column(TypeName = "varchar(1000)")]
        public string? Comment { get; set; }

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
