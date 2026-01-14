using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SendBillBL.Entity;
using System.IO;
using System.ComponentModel;

namespace SendBillBL
{
    public class BillInfo
    {
        public BillInfo()
        {


        }

        public bool SendMail(MailPoco mailPoco)
        {
            string smtpServer = mailPoco.smtpServer;//"smtp.office365.com";//"smtpout.secureserver.net";
            int smtpPort = mailPoco.smtpPort;//587; // or the appropriate port for your SMTP server
            string smtpUsername = mailPoco.smtpUsername; //"contact@nettoyageramssis.ca";
            string smtpPassword = mailPoco.smtpPasswor;//"VU21mw81!";

            try
            {
                // Create and configure the SMTP client
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 10000;
                //Attachment attachment = new Attachment(attachementlist.FirstOrDefault());

                // Create and configure the email message""
                MailMessage mailMessage = new MailMessage(mailPoco.From, "contact@nettoyageramssis.ca", mailPoco.Subject, mailPoco.Body);//((mailPoco.From, To.Text, Subject.Text, Message.Text);mailPoco.To

                mailMessage.IsBodyHtml = false;
                //mailMessage.Body += imageHtml;
                mailMessage.Attachments.Add(new Attachment(mailPoco.Path));
               // smtpClient.SendMailAsync(mailMessage);
               smtpClient.Send(mailMessage);

                

            }
            catch (SmtpException ex)
            {
                // Journalisation spécifique pour les erreurs SMTP
                Console.WriteLine($"Erreur SMTP: {ex.StatusCode} - {ex.Message}");
                throw new ApplicationException("Échec de l'envoi de l'email via SMTP. Vérifiez les paramètres du serveur.", ex);
            }
            catch (Exception ex)
            {
                // Journalisation des autres erreurs
                Console.WriteLine($"Erreur générale: {ex.Message}");
                throw new ApplicationException("Une erreur inattendue s'est produite lors de l'envoi de l'email.", ex);
            }

            return true;


        }
    }
}
