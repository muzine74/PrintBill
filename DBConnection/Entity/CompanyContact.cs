namespace DBConnection.Entity
{
    public class CompanyContact
    {
        public Guid   ContactId { get; set; }
        public Guid   CompanyId { get; set; }
        public Guid   TenantId  { get; set; }
        public string Name      { get; set; } = string.Empty;
        public string? Mail     { get; set; }
        public string? Phone    { get; set; }
        public string? Notes    { get; set; }
        public bool   IsActive  { get; set; } = true;

        public virtual Company Company { get; set; } = null!;
    }
}
