﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bisopi___Proyectos.Models
{
    public class Deal
    {
        [Key]
        public Guid DealID { get; set; }

        [Display(Name = "Lead")]
        public Guid? LeadID { get; set; }

        [Display(Name = "Deal")]
        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string DealName { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? ClientID { get; set; }

        [Display(Name = "Responsable Cliente")]
        [Column(TypeName = "varchar(200)")]
        public string? ResponsibleClient { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? ProposalStatusID { get; set; }

        [Display(Name = "Moneda")]
        public Guid? CurrencyID { get; set; }

        [Display(Name = "Valor")]
        public double? LeadValue { get; set; }

        [Display(Name = "Preventa asignado")]
        [Column(TypeName = "varchar(200)")]
        public Guid? AssignedPresaleID { get; set; }

        [Display(Name = "Fecha de entrega propuesta")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProposedDeliveryDate { get; set; }

        [Display(Name = "Facturable (Si/No)")]
        public bool Billable { get; set; }

        [Display(Name = "Hitos planeados")]
        public double? PlannedMilestones { get; set; }

        [Display(Name = "Total Horas")]
        public double? TotalHours { get; set; }

        [Display(Name = "Costo Total Deal")]
        public double? ProjectCost { get; set; }

        [Display(Name = "Margen Bruto")]
        public double? GrossMargin { get; set; }

        [Display(Name = "Descuento Comercial")]
        public double? CommercialDiscount { get; set; }

        [Display(Name = "Comentarios")]
        [Column(TypeName = "varchar(1000)")]
        public string? Comments { get; set; }

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

        [ForeignKey("ClientID")]
        public Client Client { get; set; }

        [ForeignKey("ProposalStatusID")]
        public ProposalStatus ProposalStatus { get; set; }

        [NotMapped]
        public double? ProjectedRm { get; set; }

        [NotMapped]
        public string? Abbreviation { get; set; }

        [NotMapped]
        public string pripriority { get; set; }

    }
}
