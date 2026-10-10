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
        /// <summary>Charge qui revient chaque mois (ex. loyer, assurance) ; false = charge ponctuelle.</summary>
        public bool     IsMonthly       { get; set; }
        /// <summary>Le montant saisi contient déjà les taxes (true) ou est hors taxes (false).</summary>
        public bool     TaxIncluded     { get; set; }
        public DateTime CreatedAt       { get; set; } = DateTime.UtcNow;

        /// <summary>Date de la charge (jour sans heure). Pour une charge mensuelle : la date de la première
        /// échéance ; une copie est ensuite générée chaque mois le même jour (voir ChargeRecurrence).</summary>
        public DateTime ChargeDate      { get; set; } = DateTime.Today;
        /// <summary>Renseigné sur une charge GÉNÉRÉE : la charge mensuelle (modèle) dont elle est la copie.</summary>
        public Guid?    RecurringSourceId { get; set; }
        /// <summary>Sur une charge mensuelle : date de la dernière échéance déjà générée (null = aucune encore).
        /// Une copie supprimée n'est donc jamais régénérée.</summary>
        public DateTime? RecurringThrough { get; set; }

        /// <summary>Nom du sous-dossier (sous Billing/{Fournisseur}/charges/{yyyy-MM}/) où sont
        /// stockés les documents de cette charge — dérivé du Title au moment de la création et
        /// figé ensuite (un renommage du Title ne déplace pas les fichiers déjà uploadés).
        /// Null pour les charges créées avant l'introduction de ce champ (fallback : ChargeId).</summary>
        public string?  DocumentFolder  { get; set; }

        public virtual ICollection<ChargeCompany>  ChargeCompanies  { get; set; } = new List<ChargeCompany>();
        public virtual ICollection<ChargeDocument> ChargeDocuments { get; set; } = new List<ChargeDocument>();
    }
}
