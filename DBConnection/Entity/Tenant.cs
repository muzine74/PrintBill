namespace DBConnection.Entity
{
    public class Tenant
    {
        public Guid     TenantId   { get; set; } = Guid.NewGuid();
        public string   Name       { get; set; } = string.Empty;  // "Ramssis Cleaning"
        public string   Slug       { get; set; } = string.Empty;  // "ramssis" (identifiant de connexion)
        public string   OwnerEmail { get; set; } = string.Empty;
        public string   Plan       { get; set; } = "starter";     // starter | pro | enterprise
        public bool     IsActive   { get; set; } = true;
        public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
        public DateTime? TrialEnd  { get; set; }
    }
}
