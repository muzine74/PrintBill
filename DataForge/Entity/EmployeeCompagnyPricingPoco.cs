using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Entity
{
    public class EmployeeCompagnyPricingPoco
    {
        public Guid EmployeeCompagnyPricingId { get; set; }
        public Guid CompanyPricingCalendarId { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal EmplyeePaiment { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
