using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Bisopi___Proyectos.Models
{
    public class ProjectTask
    {
        [Key]
        public Guid TaskID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tarea")]
        [Column(TypeName = "varchar(500)")]
        [MaxLength(length: 500,ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string TaskName { get; set; }

        [Display(Name = "Grupo tarea")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? TaskGroupID { get; set; }

        [ForeignKey(nameof(TaskGroupID))]
        public TaskGroup? TaskGroup { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(1000)")]
        [MaxLength(length: 1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Display(Name = "Comentario")]
        public string? Comment { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Proyecto")]
        public Guid ProjectID { get; set; }

        [ForeignKey(nameof(ProjectID))]
        public Project? Project { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Estado")]
        public Guid? TaskStatusID { get; set; }

        [ForeignKey(nameof(TaskStatusID))]
        public ProjectTaskStatus? TaskStatus { get; set; }

        [Display(Name = "Fecha inicio")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha fin")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tiempo estimado")]
        public int EstimateTime { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Horas")]
        public int ExecutionTimeHours { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Minutos")]
        public int ExecutionTimeMinutes { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tiempo ejecución")]
        public int ExecutionTime { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Cargo")]
        public Guid? PositionID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Responsable")]
        public Guid? ResponsibleID { get; set; }

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
