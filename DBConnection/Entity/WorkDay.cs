using System;

namespace DBConnection.Entity
{
    public class WorkDay
    {
        public Guid WorkDayId { get; set; }
        public DateTime Date { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
