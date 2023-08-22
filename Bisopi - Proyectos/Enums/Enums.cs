using System.ComponentModel.DataAnnotations;

namespace Bisopi___Proyectos.Enums
{
    public enum Roles
    {
        SuperAdmin,
        Admin,
    }

    public enum StatusBill
    {
        [Display(Name = "Pendeinte a Facturar")]
        PendingBilling = 10,

        [Display(Name = "Aprovado para Facturar")]
        ApprovedToBill = 20,

        [Display(Name = "Facturado")]
        Invoiced = 30
    }
}

