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
        public Guid?    OwnerCompanyId  { get; set; }   // Compagnie fournisseur — désignée parmi ChargeCompanies, optionnelle
        public string   Title           { get; set; } = string.Empty;
        public string?  Description     { get; set; }
        public decimal  Amount          { get; set; }
        public DateTime CreatedAt       { get; set; } = DateTime.UtcNow;

        public virtual Company? OwnerCompany { get; set; }

        public virtual ICollection<ChargeCompany>  ChargeCompanies  { get; set; } = new List<ChargeCompany>();
        public virtual ICollection<ChargeDocument> ChargeDocuments { get; set; } = new List<ChargeDocument>();
    }
}
