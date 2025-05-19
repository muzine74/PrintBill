using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SendBillBL.Entity;

namespace SendBillBL
{
    public class BillInfo
    {
        public BillInfo()
        {


        }

        public bool SendMail(MailPoco mailPoco)
        {
            string smtpServer = "smtp.office365.com";//"smtpout.secureserver.net";
            int smtpPort = 587; // or the appropriate port for your SMTP server
            string smtpUsername = "contact@nettoyageramssis.ca";
            string smtpPassword = "VU21mw81!";

            // Create and configure the SMTP client
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true;
            //Attachment attachment = new Attachment(attachementlist.FirstOrDefault());

            // Create and configure the email message
            MailMessage mailMessage = new MailMessage(mailPoco.From, "contact@nettoyageramssis.ca", mailPoco.Subject, mailPoco.Body);//(From.Text, To.Text, Subject.Text, Message.Text);mailPoco.To

            mailMessage.IsBodyHtml = false;
            //mailMessage.Body += imageHtml;
            mailMessage.Attachments.Add(new Attachment(mailPoco.Path));
            smtpClient.Send(mailMessage);

            return true;
        }
    }
}
