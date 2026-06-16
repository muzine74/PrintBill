using DBConnection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Entity
{
    public class EmployeeTimeLogPoco
    {
        public Guid EmployeeTimeLogId { get; set; }
        public WorkType1 WorkType1 { get; set; }
        public DateOnly Workdate { get; set; }
        public decimal ClientPrice { get; set; }

        public DateTime? BeginWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }

        // Foreign key
        public Guid CompanyId { get; set; }

        public Guid EmployeeId { get; set; }

    }
}
