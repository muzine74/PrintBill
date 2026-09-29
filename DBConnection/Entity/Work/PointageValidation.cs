using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    [Table("PointageValidations")]
    public class PointageValidation
    {
        public Guid   PointageValidationId { get; set; }
        public Guid   EmployeeId           { get; set; }

        /// <summary>Lundi de la semaine validée (format date).</summary>
        public DateOnly WeekStart          { get; set; }

        public DateTime ValidatedAt        { get; set; }
        public Guid     ValidatedByAdminId { get; set; }
        public Guid     TenantId           { get; set; }

        // Navigation
        public virtual Employee Employee { get; set; } = null!;

        /// <summary>Photo figée des compagnies affectées à l'employé au moment de la validation.
        /// L'historique d'une semaine validée se lit ici, jamais dans l'affectation actuelle.</summary>
        public virtual ICollection<PointageValidationCompany> Companies { get; set; } = new List<PointageValidationCompany>();
    }
}
