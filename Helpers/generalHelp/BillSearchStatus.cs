using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.generalHelp
{
    public class BillSearchStatus
    {
        public string keysearch { get; set; } 
        public bool Sended { get; set; } = false;
        public bool NotSended { get; set; } = false;
        public bool payed { get; set; } = false;
        public bool notPayed { get; set; } = false;
        public DateTime? BeginDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}
