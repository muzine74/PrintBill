using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Historique des enregistrements d'un paiement employé (page « Jours travaillés / Revenus ») :
    // une ligne par clic « Enregistrer », avec l'écart d'argent par rapport à la valeur précédente.
    // Lié à l'employé + la période (pas au paiement) : la suppression définitive d'un employé l'efface.
    [Table("EmployeePaymentHistories")]
    public class EmployeePaymentHistory
    {
        public Guid EmployeePaymentHistoryId { get; set; }
        public Guid TenantId   { get; set; }
        public Guid EmployeeId { get; set; }

        public DateOnly PeriodStart { get; set; }
        public DateOnly PeriodEnd   { get; set; }

        public DateTime ChangedAt { get; set; }       // UTC
        public string?  ChangedBy { get; set; }       // identifiant de l'utilisateur

        public decimal? PreviousAmount { get; set; }  // null = premier enregistrement
        public decimal  NewAmount      { get; set; }
        public decimal  Delta          { get; set; }  // NewAmount - (PreviousAmount ?? 0), positif ou négatif

        public string?  PaidDates { get; set; }       // journées cochées à cette date (yyyy-MM-dd,…)
        public string?  Note      { get; set; }       // raison saisie à cette date
        public DateOnly? PaymentDate   { get; set; }  // date de paiement choisie à cette date
        public bool?     IsTransferred { get; set; }
        public decimal?  TpsAmount     { get; set; }  // taxes figées à cette date (null = ancien enregistrement)
        public decimal?  TvqAmount     { get; set; }  // case « Paiement transféré » à cette date (null = ancien enregistrement)

        public virtual Employee Employee { get; set; }
    }
}
