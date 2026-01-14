using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class BillHistory
    {
        public Guid Id { get; set; }
        public string BillNumber { get; set; }

        [Key]
        public int billIdentifier { get; set; }
        public string compagnyName { get; set; }
        public string compagnyCode { get; set; }
        public string MouthBill { get; set; }
        public DateTime BilledDate { get; set; }
        public string? BillDescriptionText { get; set; } // Renamed to avoid conflict
        public float compagnyPrice { get; set; }
        public int NumberOfVisite { get; set; }
        public float TotalWithOutTax { get; set; }
        public float TPS { get; set; }
        public float TVQ { get; set; }
        public float TotalWithTax { get; set; }
        public string BillPath { get; set; }
        public string? AfterSendedBillPath { get; set; }
        public bool? Issended { get; set; }
        public bool? IsPayed { get; set; }
        public string? BillHistoryNote { get; set; }


        public ICollection<BillDescription> BillDescriptions { get; set; }
         = new List<BillDescription>();
    }
}   
