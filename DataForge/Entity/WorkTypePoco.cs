using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnection.Entity;

namespace DataBridge
{
    public class WorkTypePoco
    {
        public List<WorkType> WorkTypesLst;

        public WorkTypePoco()
        {
            WorkTypesLst = new List<WorkType>();
        }
    }
}
