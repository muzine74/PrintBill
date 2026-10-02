using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class EmployeeCompany
    {        
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
        public string? Note { get; set; }
        /// <summary>Taux horaire payé spécifique à cet employé pour cette compagnie (null = taux par défaut de la compagnie).</summary>
        public decimal? HourlyRate { get; set; }
        /// <summary>Mode de rémunération chez CETTE compagnie : "Hourly" / "Visit" ; null = mode par défaut de l'employé.</summary>
        public string? PayMode { get; set; }


        // Navigation properties
        public virtual Employee Employee { get; set; }
        public virtual Company Company { get; set; }
    }
}
