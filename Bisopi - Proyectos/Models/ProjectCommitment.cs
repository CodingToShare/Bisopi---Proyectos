using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class ProjectCommitment
    {
        [Key]
        public Guid ProjectCommitmentID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Proyecto")]
        public Guid ProjectID { get; set; }

        [ForeignKey(nameof(ProjectID))]
        public Project Project { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Compromiso")]
        [Column(TypeName = "varchar(500)")]
        [MaxLength(length: 500, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string? ProjectCommitmentName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nro. Compromiso")]
        public int? CommitmentNumber { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Responsable")]
        [Column(TypeName = "varchar(500)")]
        [MaxLength(length: 500, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string? Responsible { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Estado")]
        public Guid? TaskStatusID { get; set; }

        [ForeignKey(nameof(TaskStatusID))]
        public ProjectTaskStatus TaskStatus { get; set; }

        [Display(Name = "Fecha Planeada")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? PlannedDate { get; set; }

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
