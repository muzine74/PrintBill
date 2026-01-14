using DBConnection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Entity
{
    public class BillDescriptionPoco
    {

        public Guid BillDescriptionPocoId { get; set; }

        public int BillHistoryIdPoco { get; set; }
        public int QuantityPoco { get; set; }
        public string DescriptionPoco { get; set; }
        public float UnitPricePoco { get; set; }
        public float SubTotalPricePoco { get; set; }


        public BillHistoryPoco BillHistoryPocos { get; set; }
    }
}



