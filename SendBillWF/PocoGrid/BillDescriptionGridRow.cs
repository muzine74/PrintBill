using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBillWF.PocoGrid
{
    public class BillDescriptionGridRow
    {
        public int QuantityDtg { get; set; }
        public string DescriptionDtg { get; set; }
        public float UnitPrceDgr { get; set; }
        public float SumDtg { get; set; }
    }
}
