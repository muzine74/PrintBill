using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Lexique du relevé bancaire : un mot-clé cherché dans la description d'une transaction,
    // relié à une compagnie, un employé ou une charge (page « Relevé bancaire », onglet Lexique).
    [Table("BankLexiconEntries")]
    public class BankLexiconEntry
    {
        public Guid   BankLexiconEntryId { get; set; }
        public Guid   TenantId           { get; set; }
        public string Keyword            { get; set; } = string.Empty;
        /// <summary>« company », « employee » ou « charge ».</summary>
        public string TargetType         { get; set; } = string.Empty;
        /// <summary>CompanyId, EmployeeId ou ChargeId selon TargetType (pas de clé étrangère : trois tables possibles).</summary>
        public Guid   TargetId           { get; set; }
        public DateTime CreatedAt { get; set; }   // UTC
        public string?  CreatedBy { get; set; }
    }
}
