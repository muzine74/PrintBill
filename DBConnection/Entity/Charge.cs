using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    [Table("Charges")]
    public class Charge
    {
        public Guid     ChargeId        { get; set; } = Guid.NewGuid();
        public Guid     TenantId        { get; set; }
        public string   Title           { get; set; } = string.Empty;
        public string?  Description     { get; set; }
        public decimal  Amount          { get; set; }
        public DateTime CreatedAt       { get; set; } = DateTime.UtcNow;

        /// <summary>Nom du sous-dossier (sous Billing/{Fournisseur}/charges/{yyyy-MM}/) où sont
        /// stockés les documents de cette charge — dérivé du Title au moment de la création et
        /// figé ensuite (un renommage du Title ne déplace pas les fichiers déjà uploadés).
        /// Null pour les charges créées avant l'introduction de ce champ (fallback : ChargeId).</summary>
        public string?  DocumentFolder  { get; set; }

        public virtual ICollection<ChargeCompany>  ChargeCompanies  { get; set; } = new List<ChargeCompany>();
        public virtual ICollection<ChargeDocument> ChargeDocuments { get; set; } = new List<ChargeDocument>();
    }
}
