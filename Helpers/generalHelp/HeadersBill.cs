using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Helpers.pdfBill;

namespace Helpers.generalHelp
{
    public static class HeadersBill
    {        public static bool IsSingleBill { get; set; } = true;
        public static bool IsUpdate { get; set; } = false;
        public static string BillPath { get; set; }
        public static string BillCompagnieCode { get; set; }
        public static string BillHeadersDate { get;set; }

        public static DateTime? _jobDate;
        public static int BillHeadersidentifier { get;    set; }


        public static void setDateBilljobDat()
        {

        }

        public static void getDateBilljobDat()
        {
           DateFormatter dateFormatter = new DateFormatter();
            
                string dateString = HeadersBill.BillHeadersDate;
                string[] dateParts = dateString.Split(' ');
                int day = int.Parse(dateParts[1].TrimEnd(','));
                int month = dateFormatter.getCorrecteNameOfMouth(dateParts[0]);
                int year = int.Parse(dateParts[2]);
                HeadersBill._jobDate = new DateTime(year, month, day);
        }

        public static DateTime getBilledDate()
        {
            return HeadersBill._jobDate.HasValue
                     ? HeadersBill._jobDate.Value
                     : default(DateTime);

        }

        public static DateTime endMounth()
        {
            DateTime date = getBilledDate();

            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)).AddDays(1).AddTicks(-1);

        }
    }
}
