using DBConnection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.DTO
{
    public class TimeLogQueryResultDto
    {

        public Guid EmployeeId { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Note { get; set; }
        public Guid TimeLogId { get; set; }
        public DateOnly WorkDate { get; set; }
        public DateTime? BeginWork { get; set; }
        public DateTime? EndWork { get; set; }
        public decimal ClientPrice { get; set; }
        public WorkType1 WorkType { get; set; }
    }
}
