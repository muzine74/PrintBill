using System.Collections.Generic;

namespace DBConnection.Entity
{
    public class Employee
    {
        public Employee() {
        }
       
        public Guid EmployeeId { get; set; }
        public string NAS { get; set; }
        public string name { get; set; }
        public string? EmployeeMail { get; set; }
        public string? EmployeePhone { get; set; }
        public string? notes { get; set; }
        public bool   IsActive     { get; set; } = true;
        public string EmployeeType { get; set; } = "Permanent";
        /// <summary>Mode de rémunération : "Hourly" = par heure ; null (ou autre) = par visite (planning).</summary>
        public string? PayMode { get; set; }
        /// <summary>Numéros de taxes de l'employé (travailleur autonome). Renseigné = la taxe s'ajoute à ses paiements.</summary>
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string? TpsNumber { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string? TvqNumber { get; set; }
        /// <summary>Chef d'équipe : peut superviser des employés et d'autres chefs d'équipe.</summary>
        public bool    IsTeamLead { get; set; }
        /// <summary>Chef d'équipe / responsable direct (obligatoire, sauf pour un chef au sommet de la hiérarchie).</summary>
        public Guid?   ManagerId  { get; set; }
        /// <summary>Design de couleurs choisi par l'utilisateur ("nuit", "ocean", "jour", "ciel") ; null = défaut.</summary>
        public string? UiTheme    { get; set; }
        public Guid   TenantId     { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        // Navigation property for many-to-many with Company
        public virtual ICollection<EmployeeCompany> EmployeeCompanies { get; set; } = new List<EmployeeCompany>();

        // Navigation property for assignments
       

        public virtual ICollection<EmployeeFile> EmployeeFiles { get; set; } = new List<EmployeeFile>();
    }
}
