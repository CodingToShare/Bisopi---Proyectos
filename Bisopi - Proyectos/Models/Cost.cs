using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class Cost
    {
        [Key]
        public Guid CostID { get; set; }

        [Display(Name = "Usuario")]
        public Guid? UserID { get; set; }

        [Display(Name = "Antigüedad")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? SeniorityID { get; set; }

        [Display(Name = "Fecha Desde")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "Fecha Hasta")]
        public DateTime? DateUntil { get; set; }

        [Display(Name = "Valor Hora")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? ProjectValue { get; set; }

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
