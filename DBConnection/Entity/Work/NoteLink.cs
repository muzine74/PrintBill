using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    /// <summary>Lien d'une note vers un employé, une compagnie ou une facture.</summary>
    [Table("NoteLinks")]
    public class NoteLink
    {
        public Guid   NoteLinkId { get; set; }
        public Guid   NoteId     { get; set; }
        /// <summary>"employee" | "company" | "bill"</summary>
        public string EntityType { get; set; } = string.Empty;
        /// <summary>EmployeeId, CompanyId ou BillHistories.Id</summary>
        public Guid   EntityId   { get; set; }

        public virtual Note Note { get; set; } = null!;
    }
}
