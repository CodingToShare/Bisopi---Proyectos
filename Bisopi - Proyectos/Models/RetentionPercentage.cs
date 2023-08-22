using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class RetentionPercentage
    {
        [Key]
        public Guid RetentionPercentageID { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? CountryID { get; set; }

        [Display(Name = "% Retención")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? Retention { get; set; }

        [Display(Name = "Vr. IVA")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? ValueAddedTax { get; set; }

        [Display(Name = "Vr. RETE IVA")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? ValueAddedTaxWuthholding { get; set; }

        [Display(Name = "Fecha Inicio Vigencia")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? EffectiveStartDate { get; set; }

        [Display(Name = "Fecha Fin Vigencia")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? ValidEndDate { get; set; }

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

        [ForeignKey("CountryID")]
        public Country? Country { get; set; }
    }
}
