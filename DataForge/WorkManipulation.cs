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
    public class WorkManipulation
    {
        public WorkTypePoco WorkTypePoco;
        public WorkPoco workPoco = new WorkPoco();
        RamssisCleaningContex ramssisCleaningContex;
        DbContextOptions<RamssisCleaningContex> options;
        public List<DateOnly> jours = new List<DateOnly>();
        ReportBridgeDTO reportBridgeDTO = new ReportBridgeDTO();

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

            List<DateOnly> djoursString = jours.Select(d => d).ToList();

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



            //remplir les nouveau

            foreach (var dtv in dtvToWorkManipPoco)
            {


                Guid companyId = ramssisCleaningContex.Companies
                    .Where(c => c.companyCode == dtv.compagnyCode)
                    .Select(c => c.CompanyId)
                    .FirstOrDefault();

                var Emplyeepaiment = GetWorkPrice(workPoco.employeeLst[0].EmployeeId, companyId)
                        .Where(t => t.CompanyId == companyId && t.EmployeeId == workPoco.employeeLst[0].EmployeeId
                        && t.Days.Contains(ConvertStringToDateandGetDays(dtv.workdate.ToString("ddMMyyyy")))).Select(e => e.Emplyeepaiment).ToList().FirstOrDefault();

                // var tes2 = ConvertStringToDateandGetDays(dtv.workdate);


                if (companyId == Guid.Empty)
                    continue;

                Work wk = new Work
                {
                    WorkId = Guid.NewGuid(),
                    Description = "",
                    Workdate = dtv.workdate,
                    ClientPrice = Emplyeepaiment,
                    CompanyId = companyId,
                    EmployeeId = workPoco.employeeLst[0].EmployeeId
                };

                ramssisCleaningContex.Works.Add(wk);
            }
            ramssisCleaningContex.SaveChanges();
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

        public void GetAllWorkTypes()
        {
            WorkTypePoco.WorkTypesLst = ramssisCleaningContex.WorkTypes.ToList();

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
                                               Emplyeepaiment = p.Emplyeepaiment,
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
                    item.Emplyeepaiment = employeeCompagnyPricing.EmplyeePaiment;
                }
            }

            return employeeCompagnyPricingPoco;
        }

        private string ConvertStringToDateandGetDays(string fdate)
        {
            DateTime date = DateTime.ParseExact(fdate, "ddMMyyyy", CultureInfo.InvariantCulture);

            return date.ToString("dddd", CultureInfo.InvariantCulture);

        }

        public void GetWorkListByCompagnyEmployeeId(ReportBridgeDTO reportBridgeDTO)
        {
            workPoco.workLst.Clear();

            var result = ramssisCleaningContex.Works
                .Where(w => w.CompanyId.Equals(reportBridgeDTO.CompagnieID)
                                && w.EmployeeId.Equals(reportBridgeDTO.EmployeeID)
                                && w.Workdate >= reportBridgeDTO.BeginDate && w.Workdate <= reportBridgeDTO.EndDate
                                )
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