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

        public virtual ICollection<CompanyContact> CompanyContacts { get; set; } = new List<CompanyContact>();
        public virtual ICollection<EmployeeCompany> EmployeeCompanies { get; set; } = new List<EmployeeCompany>();
        public virtual ICollection<CompanyPricingCalendar> CompanyPricingCalendars { get; set; } = new List<CompanyPricingCalendar>();
    }
}
