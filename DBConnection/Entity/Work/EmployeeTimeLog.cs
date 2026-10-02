using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public enum WorkType1
    {
        Visit,      // Paid per visit
        Hourly,     // Paid per hour
        FlatRate    // Fixed amount
    }

    [Table("EmployeeTimeLogs")]
    public class EmployeeTimeLog
    {
        public Guid EmployeeTimeLogId { get; set; }
        public WorkType1 WorkType1 { get; set; }
        public DateOnly Workdate { get; set; }
        public decimal ClientPrice { get; set; }

        public DateTime? BeginWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }
        /// <summary>Pointage horaire : taux client figé à l'enregistrement (facturation). ClientPrice = paie de l'employé.</summary>
        public decimal? ClientHourlyRate { get; set; }
        /// <summary>Employé payé à l'heure : taux payé figé à l'enregistrement (null = payé à la visite).</summary>
        public decimal? PayHourlyRate { get; set; }
        /// <summary>Saisie en heures : total des plages de la journée (BeginWorkDate = début de la 1re, EndWorkDate = fin de la dernière).</summary>
        public decimal? WorkedHours { get; set; }
        /// <summary>Détail des plages de la journée : "08:00-11:30;13:00-15:00".</summary>
        public string? TimeRanges { get; set; }

        // Foreign key
        public Guid CompanyId { get; set; }

        public Guid EmployeeId { get; set; }
        public Guid TenantId   { get; set; }

        // Navigation properties
        public virtual Company  Company  { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
