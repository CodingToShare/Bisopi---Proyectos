using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class Country
    {
        [Key]
        public Guid CountryID { get; set; }

        [Display(Name = "Pais")]
        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string CountryName { get; set; }

        [Display(Name = "Abreviatura")]
        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Abbreviation { get; set; }

        [NotMapped]
        public string CountryNameAndAbbreviation
        {
            get{ return countryNameAndAbbreviation = $"{CountryName} ({Abbreviation.Trim()})"; ;}
            set { countryNameAndAbbreviation = $"{CountryName} ({Abbreviation.Trim()})"; }
        }

        private string countryNameAndAbbreviation;

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
