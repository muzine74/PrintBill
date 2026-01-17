using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class EmployeeWork
    {
        public Guid EmployeeWorkId { get; set; }

        // Références
        public Guid CompanyWorkId { get; set; }
        public Guid EmployeeId { get; set; }

        // Date et heure du travail
        public DateTime WorkDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }

        // Informations sur le travail effectué
        public decimal HoursWorked { get; set; }
        public decimal Amount { get; set; }

        // Statut (planifié, effectué, payé, etc.)
        public WorkStatus Status { get; set; } = WorkStatus.Completed;

        // Notes optionnelles
        public string? Notes { get; set; }

        // Relations
        public CompanyWork CompanyWork { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }

    public enum WorkStatus
    {
        Scheduled,      // Planifié
        InProgress,     // En cours
        Completed,      // Terminé
        Cancelled,      // Annulé
        Paid           // Payé
    
    }
}
