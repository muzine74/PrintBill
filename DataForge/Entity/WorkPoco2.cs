using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnection.Entity;

namespace DataBridge.Entity
{
    public class WorkPoco2
    {
        public CompanyWork companyWork; 
        public EmployeeWork employeeWork;
        public Work work;
        public WorkHour workHour;
        public WorkSchedule workSchedule;
        public WorkType workType;

        public WorkPoco2()
        {
            companyWork = new CompanyWork();
            employeeWork = new EmployeeWork();
            workHour = new WorkHour();
            workSchedule = new WorkSchedule();
            workType = new WorkType();
            work = new Work();                
        }
    }
}
