using Bisopi___Proyectos.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class InvoiceReport
    {
        [Key]
        public Guid InvoiceReportID { get; set; }

        [Display(Name = "Factura")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid BillID { get; set; }

        [Display(Name = "Hito")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid MilestoneID { get; set; }

        [Display(Name = "Proyecto")]
        public Guid? ProjectID { get; set; }

        [Display(Name = "Proyecto")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? ProjectName { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? CountryID { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? ClientID { get; set; }

        [Display(Name = "Estado Proyecto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? ProjectStatusID { get; set; }

        [Display(Name = "¿Es control de Cambio?")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool IsItChangeControl { get; set; }

        [Display(Name = "Nro. Hito")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? MilestoneNumber { get; set; }

        [Display(Name = "Fecha Hito")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime MilestoneDate { get; set; }

        [Display(Name = "Estado Factura")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public StatusBill StatusBill { get; set; }

        [Display(Name = "Moneda")]
        public Guid? CurrencyID { get; set; }

        [Display(Name = "Valor Hito")]
        public double? Value { get; set; }

        [NotMapped]
        [Display(Name = "TRM Proyectada")]
        public double? TRM { get; set; }

        [NotMapped]
        [Display(Name = "Subtotal Factura en Pesos")]
        public double? SubTotalBillCOP { get; set; }

        [NotMapped]
        [Display(Name = "Vr. Retención en Pesos")]
        public double? RetentionValueCOP { get; set; }

        [NotMapped]
        [Display(Name = "Vr. IVA")]
        public double? ValueAddedTax { get; set; }

        [NotMapped]
        [Display(Name = "Vr. RETE IVA")]
        public double? ValueAddedTaxWuthholding { get; set; }

        [NotMapped]
        [Display(Name = "Total Factura en Pesos")]
        public double? TotalBillCOP { get; set; }

        [Display(Name = "Fecha Aprobado Facturar")]
        public DateTime? BillingApprovedDate { get; set; }

        [Display(Name = "Fecha Facturado")]
        public DateTime? BilledDate { get; set; }

        [Display(Name = "Fecha Pagado")]
        public DateTime? DatePaid { get; set; }

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

        [ForeignKey("CurrencyID")]
        public Currency Currency { get; set; }
    }
}
