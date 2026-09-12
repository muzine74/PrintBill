using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class EmployeeCredential
    {
        public int    EmployeeCredentialId { get; set; }
        public string Username             { get; set; }
        public string PasswordHash         { get; set; }
        public Guid   EmplyeeID            { get; set; }
        public Guid   TenantId             { get; set; }
        /// <summary>Super utilisateur — accès global à tous les tenants.</summary>
        public bool   IsSuperUser          { get; set; } = false;

        /// <summary>SHA-256 (hex) du jeton de réinitialisation envoyé par courriel — jamais
        /// le jeton en clair. Null si aucune réinitialisation n'est en cours.</summary>
        public string? ResetTokenHash        { get; set; }
        /// <summary>Expiration du jeton ci-dessus — un jeton expiré ou absent est refusé.</summary>
        public DateTime? ResetTokenExpiresAt { get; set; }
    }
}
