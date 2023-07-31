using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bisopi___Proyectos.Models
{
    public class QuoteStatus
    {
        [Key]
        public Guid QuoteStatusID { get; set; }

        [Display(Name = "Estado Cotizacion")]
        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string QuoteStatusName { get; set; }

        [Display(Name = "Abreviatura")]
        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Abbreviation { get; set; }

        [NotMapped]
        public string QuoteStatusNameAndAbbreviation
        {
            get { return quoteStatusAndAbbreviation = $"{QuoteStatusName} ({Abbreviation.Trim()})"; ; }
            set { quoteStatusAndAbbreviation = $"{QuoteStatusName} ({Abbreviation.Trim()})"; }
        }

        private string quoteStatusAndAbbreviation;

        [Display(Name = "Orden")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Order { get; set; }

        [Display(Name = "Color")]
        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Color { get; set; }

        [Display(Name = "¿Visible?")]
        public bool Visible { get; set; }

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
