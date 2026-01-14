using DBConnection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Entity
{
    public class WorkSchedule
    {
        public Guid CompanyWorkId { get; set; }

        public Guid CompanyId { get; set; }
        public int WorkTypeId { get; set; }

        public decimal? HourlyRate { get; set; }
        public decimal? WeeklyRate { get; set; }
        public decimal? MonthlyRate { get; set; }

        public Company Company { get; set; } = null!;
        public WorkType WorkType { get; set; } = null!;

        public ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
        public ICollection<WorkHour> WorkHours { get; set; } = new List<WorkHour>();


    }
}
