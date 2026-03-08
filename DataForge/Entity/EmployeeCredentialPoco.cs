using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Entity
{
    public class EmployeeCredentialPoco
    {
        public int EmployeeCredentialId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Guid EmplyeeID { get; set; }

        public List<CompagniePoco> EmployeeCompagnies { get; set; }
    }
}
