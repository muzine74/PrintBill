using DBConnection.Entity;

namespace DBConnection.Entity
{
    public class EmployeeFile
    {
        public Guid     EmployeeFileId { get; set; } = Guid.NewGuid();
        public Guid     EmployeeId     { get; set; }
        public string   FileName       { get; set; } = string.Empty;   // nom sur disque
        public string   OriginalName   { get; set; } = string.Empty;   // nom d'origine
        public DateTime UploadedAt     { get; set; } = DateTime.UtcNow;

        public virtual Employee Employee { get; set; } = null!;
    }
}
