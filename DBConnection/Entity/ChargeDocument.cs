using System;

namespace DBConnection.Entity
{
    public class ChargeDocument
    {
        public Guid     ChargeDocumentId { get; set; } = Guid.NewGuid();
        public Guid     ChargeId         { get; set; }
        public string   FileName         { get; set; } = string.Empty;   // nom sur disque
        public string   OriginalName     { get; set; } = string.Empty;   // nom d'origine
        public DateTime UploadedAt       { get; set; } = DateTime.UtcNow;

        public virtual Charge Charge { get; set; } = null!;
    }
}
