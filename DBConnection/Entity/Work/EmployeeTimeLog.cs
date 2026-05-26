using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public enum WorkType1
    {
        Visit,      // Paid per visit
        Hourly,     // Paid per hour
        FlatRate    // Fixed amount
    }

    [Table("EmployeeTimeLogs")]
    public class EmployeeTimeLog
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
        public Guid TenantId   { get; set; }

        // Navigation properties
        public virtual Company  Company  { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
