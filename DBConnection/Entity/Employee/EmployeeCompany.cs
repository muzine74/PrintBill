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


        // Navigation properties
        public virtual Employee Employee { get; set; }
        public virtual Company Company { get; set; }
    }
}
