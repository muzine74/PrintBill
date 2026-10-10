using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Montant réellement payé à un employé pour une période donnée (from/to), saisi
    // manuellement par un admin — distinct du "gain cumulé" (calculé depuis EmployeeTimeLogs).
    // Depuis 2026-10 : UNE LIGNE PAR VERSEMENT. Une semaine (PeriodStart = lundi, PeriodEnd = dimanche) peut
    // en avoir plusieurs ; le total versé pour la semaine est la somme de ses lignes.
    [Table("EmployeePayments")]
    public class EmployeePayment
    {
        public Guid EmployeePaymentId { get; set; }
        public Guid TenantId          { get; set; }
        public Guid EmployeeId        { get; set; }

        public DateOnly PeriodStart { get; set; }
        public DateOnly PeriodEnd   { get; set; }

        /// <summary>Montant AVANT taxes. Montant réel transféré = AmountPaid + TpsAmount + TvqAmount.</summary>
        public decimal  AmountPaid { get; set; }
        /// <summary>Taxes figées à l'enregistrement (selon les numéros TPS / TVQ de l'employé ce jour-là).
        /// NULL = paiement antérieur à cette fonction (taxes recalculées d'après le profil actuel).</summary>
        public decimal? TpsAmount  { get; set; }
        public decimal? TvqAmount  { get; set; }
        public string?  Note       { get; set; }

        /// <summary>Journées payées (yyyy-MM-dd séparées par des virgules) cochées sur la page
        /// « Jours travaillés / Revenus ». NULL = non mémorisé (ancien paiement ou autre page).</summary>
        public string?  PaidDates  { get; set; }

        /// <summary>Date de paiement choisie par l'utilisateur (calendrier). NULL = non renseignée.</summary>
        public DateOnly? PaymentDate   { get; set; }
        /// <summary>Case « Paiement transféré » : le virement a réellement été fait.</summary>
        public bool      IsTransferred { get; set; }
        public DateTime? TransferredAt { get; set; }   // UTC, moment où la case a été cochée

        public DateTime UpdatedAt { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
