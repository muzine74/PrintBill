using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    [Table("Notes")]
    public class Note
    {
        public Guid     NoteId      { get; set; }
        public Guid     TenantId    { get; set; }
        public Guid     CreatedByEmployeeId { get; set; }
        public string   Title       { get; set; } = string.Empty;
        public string   Description { get; set; } = string.Empty;
        public bool     IsActive    { get; set; } = true;
        public bool     IsDeleted   { get; set; } = false;
        public DateTime CreatedAt   { get; set; }

        // Entités auxquelles la note est rattachée (0..n, types mélangés possibles).
        // Une note active liée s'affiche en alerte lors des actions sur l'une de ces entités.
        // (Les anciennes colonnes Notes.LinkedEntityType/LinkedEntityId restent en base, non utilisées.)
        public virtual ICollection<NoteLink> Links { get; set; } = new List<NoteLink>();
    }
}
