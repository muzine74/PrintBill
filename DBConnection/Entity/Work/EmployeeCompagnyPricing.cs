using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class EmployeeCompagnyPricing
    {
        public Guid EmployeeCompagnyPricingId { get; set; }
        public Guid CompanyPricingCalendarId { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal EmployeePayment { get; set; }
        public bool IsActive { get; set; }  = false;
        public DateTime ApplicatedDate { get; set; } = DateTime.Now;
    }
}

