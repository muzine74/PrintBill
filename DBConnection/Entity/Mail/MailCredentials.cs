using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity.Mail
{
    public class MailCredential
    {
        public Guid CompanyId { get; set; }
        public string? smtpServer { get; set; }
        public int? smtpPort { get; set; }
        public string? smtpUsername { get; set; }
        public string? smtpPassword { get; set; }

        // Navigation property back to Company
        public virtual Company Company { get; set; }
    }
}
