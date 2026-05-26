using DataBridge.DTO;
using DataBridge.Entity;
using DataBridge.Helpers;
using DBConnection;
using DBConnection.Entity;
using Helpers.PocoGrid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataBridge
{
    public class WorkManipulation : ManipulationBase
    {
        public WorkPoco workPoco = new WorkPoco();
        public List<DateOnly> jours = new List<DateOnly>();
        ReportBridgeDTO reportBridgeDTO = new ReportBridgeDTO();

        public WorkManipulation() : base()
        {
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
        }

        public void initValuefromCompagnyId(CompagniePoco compagniePoco)
        {

            ClearAllObjectList();
            GetCompagnyList(compagniePoco);
            GetEmployeeAssinedToCompanyList();
            GetEmployeeListByObject();
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


        public void GetWeek(DateOnly selectedDate)
        {
            jours.Clear();
            //DateTime sunday = GetSunday(selectedDate);
            //DateTime saturday = GetSaturday(selectedDate);

            DateOnly sunday = selectedDate.AddDays(-(int)selectedDate.DayOfWeek);
            DateOnly saturday = selectedDate.AddDays(-(int)selectedDate.DayOfWeek).AddDays(6);

            for (DateOnly date = sunday; date <= saturday; date = date.AddDays(1))
            {
                jours.Add(date);
            }
        }


        public List<CompanyPricingCalendar> GetCompanyPricingCalendarsList(List<CompagniePoco> CompagniesLst)
        {
            if (CompagniesLst == null || !CompagniesLst.Any())
            {
                return new List<CompanyPricingCalendar>();
            }

            var companyIds = CompagniesLst
                .Where(ec => ec != null)
                .Select(ec => ec.CompagnieID)
                .Distinct()
                .ToList();

            if (!companyIds.Any())
            {
                return new List<CompanyPricingCalendar>();
            }

            return ramssisCleaningContex.CompanyPricingCalendars
                .Where(cpc => cpc.IsActive.Equals(true) && companyIds.Contains(cpc.CompanyId)) //cpc.IsActive == true &&
                .ToList();
        }

        public EmployeeCompagnyPricing GetEmployeeCompagnyPricing(Guid cpcId, Guid empID)
        {
            return ramssisCleaningContex.EmployeeCompagnyPricings.Where(ecp => ecp.EmployeeId == empID &&
                                       ecp.CompanyPricingCalendarId == cpcId &&
                                       ecp.IsActive.Equals(true)).FirstOrDefault();

        }


        public Guid GetCompanyPricingCalendarsByDaysKey(string daysKey)
        {
            if (string.IsNullOrEmpty(daysKey))
            {
                return Guid.NewGuid();
            }

            return ramssisCleaningContex.CompanyPricingCalendars
                    .Where(cpc => cpc.IsActive.Equals(true) && cpc.Days == daysKey)
                    .Select(cpc => cpc.CompanyPricingCalendarId)
                    .FirstOrDefault();
        }

        public List<EmployeeCompagnyPricingPoco> GetWorkPrice(Guid EmplId, Guid CpmId)
        {
            CompagniManipulation compagniManipulation = new CompagniManipulation();
            List<EmployeeCompagnyPricingPoco> employeeCompagnyPricingPoco = new List<EmployeeCompagnyPricingPoco>();
            var EmployeCompagny = compagniManipulation.GetCompagnyByEmployee(EmplId);


            employeeCompagnyPricingPoco = (from c in EmployeCompagny
                                           join p in GetCompanyPricingCalendarsList(EmployeCompagny) on c.CompagnieID equals p.CompanyId
                                           where p.IsActive == true && p.DaysStatus == true
                                           select new EmployeeCompagnyPricingPoco
                                           {
                                               CompanyPricingCalendarId = p.CompanyPricingCalendarId,
                                               CompanyId = c.CompagnieID,
                                               CompagnyCode = c.CompagnieCode,
                                               CompagnyName = c.CompagnieName,
                                               EmployeeId = EmplId,
                                               EmployeePayment = p.EmployeePayment,
                                               DaysStatus = p.DaysStatus,
                                               Days = p.Days
                                           }).ToList();

            //IL faut aller cherché les paiment specilal pour les emplyéé 
            EmployeeCompagnyPricing employeeCompagnyPricing;
            foreach (var item in employeeCompagnyPricingPoco)
            {
                employeeCompagnyPricing = GetEmployeeCompagnyPricing(item.CompanyPricingCalendarId, item.EmployeeId);
                if (employeeCompagnyPricing != null)
                {
                    item.EmployeePayment = employeeCompagnyPricing.EmployeePayment;
                }
            }

            return employeeCompagnyPricingPoco;
        }



        public List<string> getCompagnyWorkinddays(Guid companyId)
        {
            return ramssisCleaningContex.CompanyPricingCalendars
                .Where(cpc => cpc.CompanyId == companyId && cpc.IsActive == true && cpc.DaysStatus == true)
                .Select(cpc => cpc.Days) // Sélectionne uniquement la colonne 'Days'
                .ToList();               // Convertit le résultat en List<string>
        }

        public List<TimeLogQueryResultDto> GetEmployeeTimeLog(Guid employeeId, DateOnly week)
        {

            workPoco.employeeCompanyLst.Clear();
            
                var(startWeek, endWeek) = HelpersExtensions.GetWeekDates(week);

           var resultQuerry = GetEmployeeTimeLogsWithDetails(employeeId : employeeId, startDate: startWeek, endDate : endWeek);

            return resultQuerry;

        }

        public List<TimeLogQueryResultDto> GetEmployeeTimeLogsWithDetails(Guid? employeeId = null, Guid? companyId = null, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            var query = from ec in ramssisCleaningContex.EmployeeCompanies
                        join c in ramssisCleaningContex.Companies
                            on ec.CompanyId equals c.CompanyId
                        from etl in ramssisCleaningContex.EmployeeTimeLogs
                            .Where(tl => tl.EmployeeId == ec.EmployeeId
                                      && tl.CompanyId == ec.CompanyId
                                      && tl.Workdate >= startDate
                                      && tl.Workdate <= endDate)
                            .DefaultIfEmpty()
                        //where ec.EmployeeId == employeeId
                        select new TimeLogQueryResultDto
                        {
                            EmployeeId = ec.EmployeeId,
                            CompanyId = ec.CompanyId,
                            CompanyName = c.companyName,
                            Note = ec.Note,
                            TimeLogId = etl != null ? etl.EmployeeTimeLogId : Guid.Empty,
                            WorkDate = etl != null ? etl.Workdate : DateOnly.MinValue,  // Date par défaut      
                            BeginWork = etl != null ? etl.BeginWorkDate : null,
                            EndWork = etl != null ? etl.EndWorkDate : null,
                            ClientPrice = etl != null ? etl.ClientPrice : 0,
                            WorkType = etl != null ? etl.WorkType1 : 0
                        };

            query = query.OrderBy(x => x.CompanyId)
                  .ThenBy(x => x.WorkDate);

            //Appliquer les filtres dynamiquement
            if (employeeId.HasValue)
                query = query.Where(x => x.EmployeeId == employeeId.Value);

            if (companyId.HasValue)
                query = query.Where(x => x.CompanyId == companyId.Value);

            //if (startDate.HasValue)
            //    query = query.Where(x => x.WorkDate >= startDate.Value);

            //if (endDate.HasValue)
            //    query = query.Where(x => x.WorkDate <= endDate.Value);

            return query.ToList();
        }
    }
}


/*
 
appeler une foction qui permet de 
compagniManipulation.GetCompagnyByEmployee(employeePoco.EmployeeId);
 GetCompanyPricingCalendarsList(List<CompagniePoco> CompagniesLst) elle va retournée CompanyPricingCalendars relier a une compagny
et apres il faut appeler  GetEmployeeCompagnyPricing(Guid cpcId, Guid empID) qui va retournée les prix de visite customiser


apres il faut mettre le prix a jour dans la liste de retour GetCompanyPricingCalendarsList(List<CompagniePoco> CompagniesLst) 
 
 */