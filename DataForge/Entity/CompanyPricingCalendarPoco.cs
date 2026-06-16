using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Entity
{
    public class CompanyPricingCalendarPoco
    {
        public Guid CompanyPricingCalendarId { get; set; }
        public string Days { get; set; }
        public bool DaysStatus { get; set; }
        public decimal CompanyBenefitPrice { get; set; }
        public decimal EmployeePayment { get; set; }
        public bool IsActive { get; set; }

    }
}
