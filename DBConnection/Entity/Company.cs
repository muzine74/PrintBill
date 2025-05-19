using System.Collections.Generic;

namespace DBConnection.Entity
{
    public class Company
    {
        public Company() {
            Employees = new List<Employee>();
            Addresses = new List<Address>();
            Clients = new List<Client>();   
        }

        public Guid CompanyId { get; set; }
        public string companyName { get; set; }
        public string companyCode { get; set; }
        public string companyStatus { get; set; }
        public string? Notes { get; set; }
        public string? Notes2 { get; set; }
        public string? prividercode { get; set; }


        public virtual ICollection<Address> Addresses { get; set; }
       public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
