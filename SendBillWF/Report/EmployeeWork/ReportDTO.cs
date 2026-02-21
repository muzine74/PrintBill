using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBillWF.Report.EmployeeWork
{
    public class ReportDTO
    {
        public string ReportName { get; set; }
        public Guid ReportId { get; set; }
        public DateOnly BeginDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Supervisor { get; set; } = "M. Hassan Aba";
    }
}
