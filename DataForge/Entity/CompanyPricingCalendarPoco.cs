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
        public decimal CopagnyBenifictPrice { get; set; }
        public decimal Emplyeepaiment { get; set; }
        public bool IsActive { get; set; }

    }
}
