using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DataBridge.Entity;
using DBConnection.Entity;


namespace DataBridge
{
    public class EmployeeManipulation
    {
        DbContextOptions<RamssisCleaningContex> options;
        RamssisCleaningContex ramssisCleaningContex;


        EmployeePoco employeePoco;

        public EmployeeManipulation()
        {
            options = new DbContextOptionsBuilder<RamssisCleaningContex>()
            .UseSqlServer("Server=DESKTOP-71ON71H\\SQLEXPRESS;Database=RamssisCleaningDB;Trusted_Connection=True;TrustServerCertificate=true;")
            .Options;

            ramssisCleaningContex = new RamssisCleaningContex(options);

        }

        public void SaveCreateEmployee(EmployeePoco emplPoco)
        {
            Employee employee = new Employee();

            employee.EmployeeId = Guid.NewGuid(); 
            employee.name = emplPoco.EmployeeName;
            employee.mail = emplPoco.EmployeeMail;
            employee.phone = emplPoco.EmployeePhone;
            employee.notes =    emplPoco.EmployeeNote;


            Address address = new Address();    

            address.AddressId = Guid.NewGuid();
            address.civicNumber = emplPoco.EmployeeCivicNumber;
            address.suite = emplPoco.EmployeeSuite;
            address.city = emplPoco.EmployeeCity;
            address.state = emplPoco.EmployeeState;
            address.country = emplPoco.EmployeeCountry;
            address.zipCode = emplPoco.EmployeeZipCode;
            address.notes = emplPoco.EmployeeAdressNote;

            EmployeeAddress employeeAddress = new EmployeeAddress();

            employeeAddress.AddressId = address.AddressId;
            employeeAddress.EmployeeId = employee.EmployeeId;

            // reste relation employee compagny a la mettre a jours

            List<CompanyEmployee> companyEmployeeList = new List<CompanyEmployee>();

            foreach (var item in emplPoco.EmployeeCompagnies)
            {
                CompanyEmployee companyEmployee = new CompanyEmployee();

                companyEmployee.EmployeeId = employee.EmployeeId;
                companyEmployee.CompanyId = (Guid)item.CompagnieID;
                companyEmployeeList.Add(companyEmployee);
            }


            ramssisCleaningContex.Employees.Add(employee);
            ramssisCleaningContex.Addresses.Add(address);
            ramssisCleaningContex.CompanyEmployees.AddRange(companyEmployeeList);

            ramssisCleaningContex.SaveChanges();

        }
    }
}
