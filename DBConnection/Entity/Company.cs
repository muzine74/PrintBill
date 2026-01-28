using DBConnection.Entity.Mail;
//using DBConnection.Entity.Tax;
using System.Collections.Generic;

namespace DBConnection.Entity
{
    public class Company
    {
        public Company() {
            Addresses = new List<Address>();
            Clients = new List<Client>();   
        }

        public Guid CompanyId { get; set; }
        public string companyName { get; set; }
        public string companyCode { get; set; }
        public bool companyStatus { get; set; }
        public string? Notes { get; set; }
        public string? Notes2 { get; set; }
        public string? prividercode { get; set; }

        public MailCredential mailCredential;

        public virtual ICollection<Address> Addresses { get; set; }

        public Guid? IdMailCredentiel { get; set; }
        // Navigation properties
        public virtual MailCredential MailCredential { get; set; }
        //public Guid? IdTaxNumber { get; set; }        
        //public virtual TaxCredential TaxCredential { get; set; }

        public string? TPSNumber { get; set; }
        public string? TVQNumber { get; set; }

        public string? WorkFrequency { get; set; }
        public string? PaymentFrequency { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        // Navigation property for many-to-many with Employee
        public virtual ICollection<EmployeeCompany> EmployeeCompanies { get; set; } = new List<EmployeeCompany>();

        // Navigation property for works  
        public virtual ICollection<Work> Works { get; set; } = new List<Work>();

        public virtual ICollection<CompanyPricingCalendar> CompanyPricingCalendars { get; set; } = new List<CompanyPricingCalendar>();

        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }

    }
}
