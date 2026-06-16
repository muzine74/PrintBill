using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge
{
    public class CompanyInfoDto
    {
        public Guid CompanyId { get; set; }
        public Guid AddressId { get; set; }
        public Guid ClientId { get; set; }
        public string CompanyName { get; set; }
        public bool CompanyStatus { get; set; }
        public string CompanyCode { get; set; }
        public string TPSNumber { get; set; }
        public string TVQNumber { get; set; }
        public string PaymentFrequency { get; set; }
        public string WorkFrequency { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Suite { get; set; }
        public string CivicNumber { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
    }
}
