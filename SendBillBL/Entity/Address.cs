using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBillBL.Entity
{
    internal class Address
    {
        public string CivicNumber { get; set; } = string.Empty;
        public string Suite { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; } = string.Empty;

    }
}
