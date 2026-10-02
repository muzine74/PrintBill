using System;
namespace DBConnection.Entity
{
    public class Company
    {
        public Guid   CompanyId     { get; set; }
        public Guid   TenantId      { get; set; }
        public string companyName   { get; set; }
        public string companyCode   { get; set; }
        public bool   companyStatus { get; set; }
        public string? Notes        { get; set; }
        public string? Notes2       { get; set; }
        public string? prividercode { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public string? TPSNumber       { get; set; }
        public string? TVQNumber       { get; set; }

        public string? WorkFrequency   { get; set; }
        public string? PaymentFrequency { get; set; }
        /// <summary>Planning bi-hebdomadaire : lundi où commence la « Semaine 1 » (puis une semaine sur deux).
        /// null = non renseigné (on ne peut pas savoir quelle semaine du calendrier est S1 ou S2).</summary>
        public DateOnly? BiWeeklyStart { get; set; }

        /// <summary>Mode de facturation : "Hourly" = par heure ; null (ou autre) = par tâche (visites, planning).</summary>
        public string?  BillingMode        { get; set; }
        /// <summary>Par heure : taux horaire facturé au client.</summary>
        public decimal? HourlyClientRate   { get; set; }
        /// <summary>Par heure : taux horaire payé à l'employé par défaut (remplacé par EmployeeCompany.HourlyRate).</summary>
        public decimal? HourlyEmployeeRate { get; set; }

        public virtual ICollection<CompanyContact> CompanyContacts { get; set; } = new List<CompanyContact>();
        public virtual ICollection<EmployeeCompany> EmployeeCompanies { get; set; } = new List<EmployeeCompany>();
        public virtual ICollection<CompanyPricingCalendar> CompanyPricingCalendars { get; set; } = new List<CompanyPricingCalendar>();
    }
}
