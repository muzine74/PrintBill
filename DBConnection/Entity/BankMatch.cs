using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Rapprochement (page « Relevé bancaire ») : une transaction du relevé associée par l'utilisateur à une facture
    // (dépôt direct / mobile, une ou plusieurs factures) ou à une charge (retrait). Une facture ou une charge n'est
    // associée qu'à une seule transaction (index uniques filtrés).
    [Table("BankMatches")]
    public class BankMatch
    {
        public Guid BankMatchId       { get; set; }
        public Guid TenantId          { get; set; }
        public Guid BankTransactionId { get; set; }

        /// <summary>« bill » ou « charge » (« employeePayment » réservé, non utilisé).</summary>
        public string TargetType { get; set; } = string.Empty;
        /// <summary>Facture : BillHistories.billIdentifier (TargetId reste null).</summary>
        public int?   BillIdentifier { get; set; }
        /// <summary>Charge : ChargeId.</summary>
        public Guid?  TargetId       { get; set; }

        /// <summary>Montant de la pièce au moment de l'association (positif).</summary>
        public decimal Amount { get; set; }

        // Effets appliqués par CE lien (annulés quand le lien est retiré)
        /// <summary>La facture a été marquée payée par l'association.</summary>
        public bool SetBillPaid    { get; set; }
        public bool SetTransferred { get; set; }   // réservé
        public bool SetPaymentDate { get; set; }   // réservé
        public bool SetValidated   { get; set; }   // réservé

        public DateTime CreatedAt { get; set; }   // UTC
        public string?  CreatedBy { get; set; }
    }
}
