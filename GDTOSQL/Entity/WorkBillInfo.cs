using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDTOSQL.Entity
{
    public class WorkBillInfo
    {
        public WorkBillInfo()
        {
                
        }
        public string CompagnyName { get; set; }
        public string CompagnyCode{ get; set; }
        public float compagnyPrice { get; set; }

        public string? JobDescription { get; set; }
        public int NumberOfVisite { get; set; }
        public float Totalprice { get; set; }
    }
}
