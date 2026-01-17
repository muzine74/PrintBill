using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class Client 
    {
 
        public Guid ClientID { get; set; }
        public string name { get; set; }
        public string? mail { get; set; }
        public string? phone { get; set; }
        public string? notes { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
