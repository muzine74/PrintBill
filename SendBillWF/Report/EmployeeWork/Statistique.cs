using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBillWF.Report.EmployeeWork
{
    public class Statistique
    {
        public int Nombrecompagny { get; set; } =0;
        public decimal NombrVIsites { get; set; } = 0;
        public decimal Totalpaiment { get; set; } = 0;
        public decimal TotalRevenue { get; set; } = 0;
        public decimal RendementEmploye { get; set; } = 0;
        public decimal RendemenetInvestissement { get; set; } = 0;
    }
}
