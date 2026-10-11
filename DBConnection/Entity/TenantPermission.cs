using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Droits (pages et actions) que le super-administrateur associe à une entreprise : une ligne par droit
    // AUTORISÉ. Une entreprise SANS aucune ligne n'est pas restreinte (tous les droits du catalogue).
    // Les administrateurs de l'entreprise ne voient et n'attribuent à leurs groupes que ces droits, et un
    // droit retiré ici ne compte plus à la connexion même s'il est encore coché dans un groupe.
    [Table("TenantPermissions")]
    public class TenantPermission
    {
        public Guid   TenantId { get; set; }
        /// <summary>Clé du catalogue (AppPermissions.Key), ex. « page.stats » ou « invoices.edit ».</summary>
        public string Key      { get; set; } = string.Empty;
    }
}
