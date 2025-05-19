using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class CompanyEmployee
    {
        

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public Guid EmployeeId { get; set; }
        public virtual  Employee Employee { get; set; }


        public float? Rate { get; set; }
        public string? Note { get; set; }
        public string? Note2 { get; set; }
    }
}
