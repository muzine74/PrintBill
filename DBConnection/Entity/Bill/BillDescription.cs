using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class BillDescription
    {

        public Guid BillDescriptionId { get; set; }
         
        public int BillHistoryId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public float UnitPrice { get; set; }
        public float SubTotalPrice { get; set; }

        // Adjusted accessibility and nullability
        public BillHistory BillHistoris { get; set; }
    }
}
