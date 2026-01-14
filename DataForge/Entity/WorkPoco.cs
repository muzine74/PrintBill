using DBConnection;
using DBConnection.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Entity
{
    public class WorkPoco
    {

        public int Id { get; set; }
        
        public List<Company> companyLst { get; set; } = new List<Company>();
        public List<Employee> employeeLst { get; set; } = new List<Employee>();
        public List<Work> workLst { get; set; } = new List<Work>();
        public List<EmployeeCompany> employeeCompanyLst { get; set; } = new List<EmployeeCompany>();


        public void ClearAllObjectList()
        {
            employeeLst.Clear();
            companyLst.Clear();
         
            workLst.Clear();
        }
    }
}
