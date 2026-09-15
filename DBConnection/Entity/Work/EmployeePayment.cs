using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Montant réellement payé à un employé pour une période donnée (from/to), saisi
    // manuellement par un admin — distinct du "gain cumulé" (calculé depuis EmployeeTimeLogs).
    [Table("EmployeePayments")]
    public class EmployeePayment
    {
        public Guid EmployeePaymentId { get; set; }
        public Guid TenantId          { get; set; }
        public Guid EmployeeId        { get; set; }

        public DateOnly PeriodStart { get; set; }
        public DateOnly PeriodEnd   { get; set; }

        public decimal  AmountPaid { get; set; }
        public string?  Note       { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
