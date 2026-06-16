using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.DTO
{
    public class ReportBridgeDTO
    {
        public string ReportName { get; set; }
        public Guid ReportId { get; set; }
        public Guid CompagnieID { get; set; }
        public Guid EmployeeID { get; set; }
        public DateOnly BeginDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Supervisor { get; set; } = "M. Hassan Aba";
    }
}
