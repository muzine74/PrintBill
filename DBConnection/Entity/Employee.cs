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
        public Guid   TenantId     { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        // Navigation property for many-to-many with Company
        public virtual ICollection<EmployeeCompany> EmployeeCompanies { get; set; } = new List<EmployeeCompany>();

        // Navigation property for assignments
       

        public virtual ICollection<EmployeeFile> EmployeeFiles { get; set; } = new List<EmployeeFile>();
    }
}
