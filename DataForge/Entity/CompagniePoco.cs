using DBConnection.Entity;
using GDTOSQL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Entity
{
    public class CompagniePoco
    {
        public Guid CompagnieID { get; set; }
        public Guid AddressID { get; set; }
        public Guid ContactID { get; set; }

        public string CompagnieName { get; set; }
        public string CompagnieCode { get; set; }
        public bool CompagnieStatus { get; set; }
        public string Compagniecountry { get; set; }
        public string CompagnieState { get; set; }
        public string Compagniecity { get; set; }
        public string CompagnieZipCode { get; set; }
        public string CompagnieSuite { get; set; }
        public string CompagnieCivicNumber { get; set; }
        public string CompagnieProvider { get; set; }
        public string ContactName { get; set; }
        public string ContactMail { get; set; }
        public string ContactPhones { get; set; }
        //public string? Compagnieprividercode { get; set; }

        public string? smtpServer { get; set; }
        public int? smtpPort { get; set; }
        public string? smtpUsername { get; set; }
        public string? smtpPassword { get; set; }

        public string? TPSNumber { get; set; }
        public string? TVQNumber { get; set; }

        public string? WorkFrequency { get; set; }
        public string? PaymentFrequency { get; set; }



        public List<WorkBillInfo> _workBillInfoList;
        public List<CompanyPricingCalendar> _companyPricingCalendar;

        public CompagniePoco()
        {
            _workBillInfoList = new List<WorkBillInfo>();
            _companyPricingCalendar = new List<CompanyPricingCalendar>();
        }

    }
}
