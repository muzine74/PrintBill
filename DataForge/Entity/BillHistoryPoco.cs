using DBConnection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Entity
{
    public class BillHistoryPoco
    {
        public Guid Id { get; set; }
        public string BillNumber { get; set; }
        public int billIdentifier { get; set; }
        public string compagnyName { get; set; }
        public string compagnyCode { get; set; }
        public string MouthBill { get; set; }
        public DateTime BilledDate { get; set; }
        public string BillDescriptionText { get; set; } // This property is causing the conflict
        public float compagnyPrice { get; set; }
        public int NumberOfVisite { get; set; }
        public float TotalWithOutTax { get; set; }
        public float TPS { get; set; }
        public float TVQ { get; set; }              
        public float TotalWithTax { get; set; }
        public string BillPath { get; set; }
        public bool Issended { get; set; }
        public bool IsPayed { get; set; }
        public string BillHistoryNote { get; set; }

        public Guid BillDescriptionId { get; set; }

        // Adjusted accessibility and nullability
        public ICollection<BillDescriptionPoco> BillDescriptionPocos { get; set; } // Renamed to avoid conflict
    }
}
