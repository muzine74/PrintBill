using System.ComponentModel.DataAnnotations;

namespace DBConnection.Entity
{
    /// <summary>Configuration par tenant (une ligne par tenant).</summary>
    public class AppConfig
    {
        [Key]
        public int  ConfigId { get; set; }
        public Guid TenantId { get; set; }

        // ── Compagnie ────────────────────────────────────────────────────────
        public string? LogoPath       { get; set; }
        public string? CompanyName    { get; set; }
        public string? CompanyAddress { get; set; }
        public string? CompanyPhone   { get; set; }
        public string? CompanyEmail   { get; set; }

        // ── SMTP ─────────────────────────────────────────────────────────────
        public string? SmtpServer   { get; set; }
        public int?    SmtpPort     { get; set; }
        public string? SmtpUser     { get; set; }
        public string? SmtpPassword { get; set; }

        // ── Taxes ────────────────────────────────────────────────────────────
        public string?  TpsNumber { get; set; }
        public string?  TvqNumber { get; set; }
        public decimal  TpsRate   { get; set; } = 5m;
        public decimal  TvqRate   { get; set; } = 9.975m;

        // ── Coordonnées bancaires ────────────────────────────────────────────
        public string? BankCoordinates { get; set; }

        // ── Contact ──────────────────────────────────────────────────────────
        public string? ContactName  { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }

        // ── Application ──────────────────────────────────────────────────────
        public string? AppVersion { get; set; }
    }
}
