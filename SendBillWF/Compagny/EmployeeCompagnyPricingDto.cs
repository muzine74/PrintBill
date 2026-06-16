using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBillWF.Compagny
{
    internal class EmployeeCompagnyPricingDto
    {
        Guid Id { get; set; }
        public Guid CompanyPricingCalendarId { get; set; }
        public Guid EmployeeId { get; set; } = Guid.Empty;
        public bool DaysStatus { get; set; }
        
        public decimal EmployeePayment { get; set; }
        public string CompagnyName { get; set; }
        public Guid CompanyId { get; set; }
        public string CompagnyCode { get; set; }
        public string Days { get; set; }


    }
}
