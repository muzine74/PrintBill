using System;

namespace DBConnection.Entity
{
    public enum WorkType1
    {
        Visit,      // Paid per visit
        Hourly,     // Paid per hour
        FlatRate    // Fixed amount
    }
    public class Work
    {

        public Guid WorkId { get; set; }   
        public string Description { get; set; }
        public WorkType1 WorkType1 { get; set; }
        public string? Workdate { get; set; }
        public decimal ClientPrice { get; set; }

        public DateTime? BeginWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }

        // Foreign key
        public Guid CompanyId { get; set; }

        public Guid EmployeeId { get; set; }
        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual Employee Employee { get; set; }
        

    }
}
