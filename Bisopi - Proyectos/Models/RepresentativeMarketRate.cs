using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class RepresentativeMarketRate
    {
        [Key]
        public Guid RepresentativeMarketRateID { get; set; }

        [Display(Name = "TRM Proyectada")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? ProjectedRm { get; set; }

        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? CurrencyID { get; set; }

        [Display(Name = "Año")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? Year { get; set; }

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

        [ForeignKey("CurrencyID")]
        public Currency? Currency { get; set; }
    }
}
