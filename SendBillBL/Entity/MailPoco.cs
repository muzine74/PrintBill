using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBillBL.Entity
{
    public class MailPoco
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public String Path { get; set; }
        public string Body { get; set; }

        public string smtpServer { get; set; }
        public int smtpPort { get; set; }
        public string smtpUsername { get; set; }
        public string smtpPasswor { get; set; }

    }
}
