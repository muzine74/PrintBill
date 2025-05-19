using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadExcelTOSQL.Data;
using LoadExcelTOSQL.Entity;


namespace DBConnection.BL
{
    internal class EmployeeRepository
    {
        RamssisCleaningContex DBContex;
        public EmployeeRepository() {
            DBContex = new RamssisCleaningContex();        
        }

        public List<Employee> GetAllEmplyees()
        {
            return (from e in DBContex.Employees
                             select e).ToList();
        }
        public List<Company> GetAllCompagnies()
        {
            return (from e in DBContex.Companies
                    select e).ToList();
        }
    }
}
