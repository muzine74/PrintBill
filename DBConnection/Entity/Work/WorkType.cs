using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class WorkType
    {
        public int WorkTypeId { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<CompanyWork> CompanyWorks { get; set; } = new List<CompanyWork>();
    }
}
