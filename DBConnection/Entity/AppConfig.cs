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
        /// <summary>Adresse civique (numéro + rue) — voir aussi CompanyCity/CompanyProvince/
        /// CompanyPostalCode/CompanyCountry pour les autres composantes de l'adresse.</summary>
        public string? CompanyAddress { get; set; }
        public string? CompanyCity       { get; set; }
        public string? CompanyProvince   { get; set; } = "QC";
        public string? CompanyPostalCode { get; set; }
        public string? CompanyCountry    { get; set; } = "Canada";
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

        /// <summary>Numéro de compte payeur ARC (format 123456789RP0001), requis sur les
        /// feuillets T4A émis par cette compagnie.</summary>
        public string? PayerAccountNumber { get; set; }

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
