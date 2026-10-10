using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Ligne d'un relevé bancaire importé depuis un fichier CSV (page « Relevé bancaire »).
    [Table("BankTransactions")]
    public class BankTransaction
    {
        public Guid BankTransactionId { get; set; }
        public Guid TenantId          { get; set; }

        public DateOnly TransactionDate { get; set; }       // colonne 4 du CSV
        public string   Description     { get; set; } = string.Empty;   // colonne 6
        public decimal? Withdrawal      { get; set; }       // colonne 8 (retrait)
        public decimal? Deposit         { get; set; }       // colonne 9 (dépôt)

        // Sert à ne pas importer deux fois la même ligne (compte + n° de ligne du relevé)
        public string?  AccountNumber   { get; set; }       // colonne 2
        public int?     LineNumber      { get; set; }       // colonne 5

        // Transaction vérifiée par un utilisateur (case « Validée » de la page)
        public bool      IsValidated { get; set; }
        public DateTime? ValidatedAt { get; set; }          // UTC
        public string?   ValidatedBy { get; set; }

        public DateTime ImportedAt { get; set; }            // UTC
        public string?  ImportedBy { get; set; }
        public string?  SourceFile { get; set; }
    }
}
