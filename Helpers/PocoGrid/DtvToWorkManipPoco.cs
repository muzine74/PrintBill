using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.PocoGrid
{
    public  class DtvToWorkManipPoco
    {
        public string compagnyCode { get; set; }
        public DateOnly workdate { get; set; }
        public Guid EmployeeID { get; set; }
    }
}
