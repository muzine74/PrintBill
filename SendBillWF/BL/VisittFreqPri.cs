using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBillWF.BL
{
    public class VisittFreqPri
    {
        public string SlectedWork { get; set; } =string.Empty;
        public string SlectedPaiment { get; set; } =string.Empty;
        public string WorkFrequencySelected { get; set; }
        public string PaimentFrequencySelected { get; set; }
        public string Days { get; set; }
        public bool DaysStatus { get; set; }
        public decimal CompanyBenefitPrice { get; set; }
        public decimal EmployeePayment { get; set; }
    }
}
