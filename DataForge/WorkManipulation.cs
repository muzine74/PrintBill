using DataBridge.Entity;
using DBConnection;
using DBConnection.Entity;
using Helpers.PocoGrid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge
{
    public class WorkManipulation
    {
        public WorkTypePoco WorkTypePoco;
        public WorkPoco workPoco = new WorkPoco();
        RamssisCleaningContex ramssisCleaningContex;
        DbContextOptions<RamssisCleaningContex> options;
        public List<DateTime> jours = new List<DateTime>();

        public WorkManipulation()
        {
            options = new DbContextOptionsBuilder<RamssisCleaningContex>()
                        .UseSqlServer("Server=DESKTOP-71ON71H\\SQLEXPRESS;Database=RamssisCleaningDB;Trusted_Connection=True;TrustServerCertificate=true;")
          .Options;

            ramssisCleaningContex = new RamssisCleaningContex(options);

            WorkTypePoco = new WorkTypePoco();
        }

        public void GetEmployeeList(EmployeePoco employeePoco)
        {
            if (employeePoco == null)
                return;
            if (employeePoco.EmployeeId == Guid.Empty)
                return;
            workPoco.employeeLst.Clear();
            var foundEmployeeLst = ramssisCleaningContex.Employees.Where(a => a.EmployeeId == employeePoco.EmployeeId).ToList();


            if (foundEmployeeLst.Count() > 0)
            {
                workPoco.employeeLst = foundEmployeeLst.ToList();
            }

        }

        public void GetWorkListByCompagnyEmployeeId()
        {
            workPoco.workLst.Clear();

            var companyIds = workPoco.companyLst.Select(c => c.CompanyId).ToList();
            var employeeIds = workPoco.employeeLst.Select(c => c.EmployeeId).ToList();
            //var companyIds = t.Select(c => c.CompanyId).ToList();

            var result = ramssisCleaningContex.Works
                .Where(w => companyIds.Contains(w.CompanyId)
                                && employeeIds.Contains(w.EmployeeId))
                .Select(w => new
                {
                    w.WorkId,
                    w.Description,
                    w.WorkType1,
                    w.Workdate,
                    w.ClientPrice,
                    w.BeginWorkDate,
                    w.EndWorkDate,
                    w.CompanyId,
                    w.EmployeeId

                })
                .ToList();

            foreach (var item in result)
            {
                Work work = new Work
                {
                    WorkId = item.WorkId,
                    Description = item.Description,
                    WorkType1 = item.WorkType1,
                    Workdate = item.Workdate,
                    ClientPrice = item.ClientPrice,
                    BeginWorkDate = item.BeginWorkDate,
                    EndWorkDate = item.EndWorkDate,
                    CompanyId = item.CompanyId,
                    EmployeeId = item.EmployeeId
                };
                workPoco.workLst.Add(work);
            }

        }
        public void GetEmployeeAssinedToCompanyList()
        {


            workPoco.employeeCompanyLst.Clear();
            if (workPoco.companyLst.Count() > 0)
            {
                var foundEmployeeCompanyLst = ramssisCleaningContex.EmployeeCompanies.Where(ce => ce.CompanyId.Equals(workPoco.companyLst[0].CompanyId)).ToList();
                if (foundEmployeeCompanyLst.Count() > 0)
                {
                    workPoco.employeeCompanyLst = foundEmployeeCompanyLst;
                }
                else
                {
                    //throw new InvalidOperationException("No EmployeeCompany records found.");
                }
            }
        }


        public void GetCompagnyAssinedToEmployeeList()
        {
            workPoco.employeeCompanyLst.Clear();
            if (workPoco.employeeLst.Count() > 0)
            {
                var foundEmployeeCompanyLst = ramssisCleaningContex.EmployeeCompanies.Where(ce => ce.EmployeeId.Equals(workPoco.employeeLst[0].EmployeeId)).ToList();
                if (foundEmployeeCompanyLst.Count() > 0)
                {
                    workPoco.employeeCompanyLst = foundEmployeeCompanyLst;
                }
                else
                {
                    //throw new InvalidOperationException("No EmployeeCompany records found.");
                }
            }
        }

        public void GetCompagnyListByObject()
        {
            workPoco.companyLst.Clear();
            foreach (var emp in workPoco.employeeCompanyLst)
            {
                var company = ramssisCleaningContex.Companies
                    .Where(a => a.CompanyId.Equals(emp.CompanyId))
                    .FirstOrDefault();

                if (company != null) // Ensure the company is not null before adding
                {
                    workPoco.companyLst.Add(company);
                }
            }
        }
        public void GetEmployeeListByObject()
        {
            workPoco.employeeLst.Clear();
            foreach (var cmp in workPoco.employeeCompanyLst)
            {
                var employee = ramssisCleaningContex.Employees
                    .Where(a => a.EmployeeId.Equals(cmp.EmployeeId))
                    .FirstOrDefault();

                if (employee != null) // Ensure the company is not null before adding
                {
                    workPoco.employeeLst.Add(employee);
                }
            }
        }

        public void ClearAllObjectList()
        {
            workPoco.ClearAllObjectList();
        }

        public void initValuefromEmployeeId(EmployeePoco employeePoco)
        {
            ClearAllObjectList();
            GetEmployeeList(employeePoco);
            GetCompagnyAssinedToEmployeeList();
            GetCompagnyListByObject();
            GetWorkListByCompagnyEmployeeId();
        }

        public void initValuefromCompagnyId(CompagniePoco compagniePoco)
        {

            ClearAllObjectList();
            GetCompagnyList(compagniePoco);
            GetEmployeeAssinedToCompanyList();
            GetEmployeeListByObject();
            GetWorkListByCompagnyEmployeeId();
        }

        //private void GetCompagnyAssinedEmployeeList()
        //{


        //    workPoco.employeeCompanyLst.Clear();
        //    if (workPoco.companyLst.Count() > 0)
        //    {
        //        var foundEmployeeCompanyLst = ramssisCleaningContex.EmployeeCompanies.Where(ce => ce.EmployeeId.Equals(workPoco.employeeLst[0].EmployeeId)).ToList();
        //        if (foundEmployeeCompanyLst.Count() > 0)
        //        {
        //            workPoco.employeeCompanyLst = foundEmployeeCompanyLst;
        //        }
        //        else
        //        {
        //            //throw new InvalidOperationException("No EmployeeCompany records found.");
        //        }
        //    }

        //}

        private void GetCompagnyList(CompagniePoco compagniePoco)
        {


            if (compagniePoco == null)
                return;
            if (compagniePoco.CompagnieID == Guid.Empty)
                return;
            workPoco.employeeLst.Clear();
            var foundECompagnyLst = ramssisCleaningContex.Companies.Where(a => a.CompanyId == compagniePoco.CompagnieID).ToList();


            if (foundECompagnyLst.Count() > 0)
            {
                workPoco.companyLst = foundECompagnyLst.ToList();
            }
        }

        public void SaveWorkLstChanege(List<DtvToWorkManipPoco> dtvToWorkManipPoco)
        {
            List<Work> Wks = new List<Work>();

            List<string> djoursString = jours.Select(d => d.ToString("ddMMyyyy")).ToList();

            var workCompanyIds = workPoco.workLst
                                    .Select(w => w.CompanyId)
                                    .Distinct()
                                    .ToList();

            var workEmployeeIds = workPoco.workLst
                                    .Select(w => w.EmployeeId)
                                    .Distinct()
                                    .ToList();


            var companyIds = ramssisCleaningContex.Companies
                                .Where(c => workCompanyIds.Contains(c.CompanyId))
                                .Select(c => c.CompanyId)
                                .ToList();

            var worksToDelete = ramssisCleaningContex.Works
                                    .Where(w =>
                                        companyIds.Contains(w.CompanyId) &&
                                        djoursString.Contains(w.Workdate) &&
                                        workEmployeeIds.Contains(w.EmployeeId)
                                    )
                                    .ToList();

            ramssisCleaningContex.Works.RemoveRange(worksToDelete);

            ramssisCleaningContex.SaveChanges();

            foreach (var dtv in dtvToWorkManipPoco)
            {
                Guid companyId = ramssisCleaningContex.Companies
                    .Where(c => c.companyCode == dtv.compagnyCode)
                    .Select(c => c.CompanyId)
                    .FirstOrDefault();

                if (companyId == Guid.Empty)
                    continue;

                Work wk = new Work
                {
                    WorkId = Guid.NewGuid(),
                    Description = "",
                    Workdate = dtv.workdate,
                    CompanyId = companyId,
                    EmployeeId = workPoco.employeeLst[0].EmployeeId
                };

                ramssisCleaningContex.Works.Add(wk);
            }
            ramssisCleaningContex.SaveChanges();
        }

        public void GetWeek(DateTime selectedDate)
        {
            jours.Clear();
            //DateTime sunday = GetSunday(selectedDate);
            //DateTime saturday = GetSaturday(selectedDate);

            DateTime sunday = selectedDate.AddDays(-(int)selectedDate.DayOfWeek);
            DateTime saturday = selectedDate.AddDays(-(int)selectedDate.DayOfWeek).AddDays(6);

            for (DateTime date = sunday; date <= saturday; date = date.AddDays(1))
            {
                jours.Add(date.Date);
            }
        }

        public void GetAllWorkTypes()
        {
            WorkTypePoco.WorkTypesLst = ramssisCleaningContex.WorkTypes.ToList();

        }

        public List<CompanyPricingCalendar> GetCompanyPricingCalendarsList(List<CompagniePoco> EmployeCompagny)
        {
            if (EmployeCompagny == null || !EmployeCompagny.Any())
            {
                return new List<CompanyPricingCalendar>();
            }

            var companyIds = EmployeCompagny
                .Where(ec => ec != null)
                .Select(ec => ec.CompagnieID)
                .Distinct()
                .ToList();

            if (!companyIds.Any())
            {
                return new List<CompanyPricingCalendar>();
            }

            return ramssisCleaningContex.CompanyPricingCalendars
                .Where(cpc => cpc.IsActive && companyIds.Contains(cpc.CompanyId))
                .ToList();
        }
    }
}
