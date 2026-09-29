using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    /// <summary>
    /// Compagnie associée à l'employé au moment où sa semaine a été validée (photo figée).
    /// Un changement d'affectation ultérieur ne modifie jamais ces lignes.
    /// </summary>
    [Table("PointageValidationCompanies")]
    public class PointageValidationCompany
    {
        public Guid    PointageValidationId { get; set; }
        public Guid    CompanyId            { get; set; }
        /// <summary>Nom de la compagnie à la validation.</summary>
        public string  CompanyName          { get; set; } = string.Empty;
        /// <summary>Note de l'affectation employé ↔ compagnie à la validation.</summary>
        public string? Note                 { get; set; }

        public virtual PointageValidation PointageValidation { get; set; } = null!;
    }
}
