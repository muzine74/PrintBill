using System;
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
    }
}
