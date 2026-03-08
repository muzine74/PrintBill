using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class EmployeeCredential
    {
        public int EmployeeCredentialId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Guid EmplyeeID { get; set; }
    }
}
