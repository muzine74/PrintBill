using System;
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

        // Navigation
        public virtual Employee Employee { get; set; } = null!;
    }
}
