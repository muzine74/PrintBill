using System.Collections.Generic;

namespace DBConnection.Entity
{
    public class Employee
    {
        public Employee() {

            Companies = new List<Company>();
            Addresses = new List<Address>();
            WorkDays = new List<WorkDay>();
        }
       
        public Guid EmployeeId { get; set; }
        public string name { get; set; }
        public string? mail { get; set; }
        public string? phone { get; set; }
        public string? notes { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<WorkDay> WorkDays { get; set; }

    }
}
