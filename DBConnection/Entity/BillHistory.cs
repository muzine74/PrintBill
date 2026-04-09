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
        public decimal compagnyPrice { get; set; }
        public int NumberOfVisite { get; set; }
        public decimal TotalWithOutTax { get; set; }
        public decimal TPS { get; set; }
        public decimal TVQ { get; set; }
        public decimal TotalWithTax { get; set; }
        public string BillPath { get; set; }
        public string? AfterSendedBillPath { get; set; }
        public bool? Issended { get; set; }
        public DateTime? SentDate { get; set; }
        public bool? IsPayed { get; set; }
        public DateTime? PaidDate { get; set; }
        public string? BillHistoryNote { get; set; }

        // Snapshot client au moment de la facturation
        public string? ClientAddress { get; set; }
        public string? ClientEmail   { get; set; }
        public string? ClientPhone   { get; set; }


        public ICollection<BillDescription> BillDescriptions { get; set; }
         = new List<BillDescription>();
    }
}   
