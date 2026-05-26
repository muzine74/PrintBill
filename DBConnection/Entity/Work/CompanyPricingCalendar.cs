using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class CompanyPricingCalendar
    {
        public Guid CompanyPricingCalendarId { get; set; }
        public Guid CompanyId { get; set; }
        public string Days { get; set; }
        public bool DaysStatus { get; set; }
        public decimal CompanyBenefitPrice { get; set; }
        public decimal EmployeePayment { get; set; }

        public DateTime ApplicatedDate { get; set; }
        public bool IsActive { get; set; }


        // Navigation property
        public Company Company { get; set; } = null!;
    }
}
