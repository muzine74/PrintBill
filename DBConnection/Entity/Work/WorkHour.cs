using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class WorkHour
    {
        public Guid WorkHourId { get; set; }

        public Guid CompanyWorkId { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public CompanyWork CompanyWork { get; set; } = null!;
    }
}
