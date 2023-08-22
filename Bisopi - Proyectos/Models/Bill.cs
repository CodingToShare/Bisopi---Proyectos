using Bisopi___Proyectos.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bisopi___Proyectos.Models
{
    public class Bill
    {
        [Key]
        public Guid BillID { get; set; }

        [Display(Name = "Hito")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid MilestoneID { get; set; }

        [Display(Name = "Estado Factura")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public StatusBill StatusBill { get; set; }

        [Display(Name = "Fecha Envío Factura")]
        public DateTime? InvoiceShipmentDate { get; set; }

        [Display(Name = "Nro. Documento Emitido")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? IssuedDocument { get; set; }

        [Display(Name = "Documento Relacionado")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? RelatedDocument { get; set; }

        [Display(Name = "Nota Factura")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? NoteInvoice { get; set; }

        [Display(Name = "Datos Factura")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? InvoiceData { get; set; }

        [Display(Name = "Concepto Factura")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? ConceptInvoice { get; set; }

        [Display(Name = "Documento Propuesta")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public string? ProposalDocument { get; set; }

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
    }
}
