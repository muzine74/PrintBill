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
        public decimal CopagnyBenifictPrice { get; set; }
        public decimal Emplyeepaiment { get; set; }

        public DateTime ApplicatedDate { get; set; }
        bool isActive { get; set; }


        // Navigation property
        public Company Company { get; set; } = null!;
    }
}
