using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bisopi___Proyectos.Models
{
    public class ProjectTaskRegistry
    {
        [Key]
        public Guid ProjectTaskRegistryID { get; set; }

        [Display(Name = "Tarea")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid ProjectTaskID { get; set; }

        [ForeignKey(nameof(ProjectTaskID))]
        public ProjectTask? ProjectTask { get; set; }

        [Display(Name = "Fecha de registro")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegistryDate { get; set; }

        public int ExecutionTime { get; set; } = 0;
        [NotMapped]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Horas")]
        public int ExecutionTimeHours { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Minutos")]
        public int ExecutionTimeMinutes { get; set; }

        public bool? McaExecution { get; set; }
        public bool? McaManual { get; set; }
        public bool? McaHistorico { get; set; }

        [Display(Name = "Comentario")]
        public string? Comment { get; set; }

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
