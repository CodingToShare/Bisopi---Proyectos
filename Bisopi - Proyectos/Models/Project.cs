using Azure.Core;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class Project
    {
        [Key]
        public Guid ProjectID { get; set; }

        [Display(Name = "Proyecto")]
        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string ProjectName { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid CountryID { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid ClientID { get; set; }

        [Display(Name = "Responsable Cliente")]
        [Column(TypeName = "varchar(500)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string CustomerManager { get; set; }

        [Display(Name = "Lider")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid LeaderID { get; set; }

        [Display(Name = "Gerente de Proyecto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid ProjectManagerID { get; set; }

        [Display(Name = "Estado Proyecto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid ProjectStatusID { get; set; }

        [Display(Name = "Tipo Proyecto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid ProjectTypeID { get; set; }

        [Display(Name = "Entregado a soporte")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid SupportStatusID { get; set; }

        [Display(Name = "Fecha de Solicitud de Estimación")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EstimateRequestDate { get; set; }

        [Display(Name = "Fecha de Respuesta")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AnswerDate { get; set; }

        [Display(Name = "Prioridad Solicitud")]
        [Column(TypeName = "varchar(200)")]
        public string RequestPriority { get; set; }

        [Display(Name = "Código Baremador")]
        [Column(TypeName = "varchar(200)")]
        public string? ScalerCode { get; set; }

        [Display(Name = "Código de Clarity")]
        [Column(TypeName = "varchar(200)")]
        public string? ClarityCode { get; set; }

        [Display(Name = "Horas Estimadas")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int EstimatedHours { get; set; }

        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha Entrega Estimada")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime EstimatedDeliveryDate { get; set; }

        [Display(Name = "Fecha Real de Finalización")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ActualCompletionDate { get; set; }

        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid CurrencyID { get; set; }

        [Display(Name = "Valor Proyecto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double ProjectValue { get; set; }

        [Display(Name = "Costo Proyecto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double ProjectCost { get; set; }

        [Display(Name = "TRM Proyecto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double TRMProject { get; set; }

        [Display(Name = "Margen Bruto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double GrossMargin { get; set; }

        [Display(Name = "Facturable (Si/No)")]
        public bool Billable { get; set; }

        [Display(Name = "Justification")]
        [Column(TypeName = "varchar(1000)")]
        public string? Justification { get; set; }

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

        [ForeignKey("CountryID")]
        public Country Country { get; set; }

        [ForeignKey("ClientID")]
        public Client Client { get; set; }

        [ForeignKey("ProjectStatusID")]
        public ProjectStatus ProjectStatus { get; set; }

        [ForeignKey("ProjectTypeID")]
        public ProjectType ProjectType { get; set; }

        [ForeignKey("SupportStatusID")]
        public SupportStatus SupportStatus { get; set; }

        [ForeignKey("CurrencyID")]
        public Currency Currency { get; set; }
    }
}
