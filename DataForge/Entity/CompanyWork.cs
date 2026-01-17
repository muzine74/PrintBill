using DBConnection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class CompanyWork
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

        // Relation many-to-many avec les employés
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
